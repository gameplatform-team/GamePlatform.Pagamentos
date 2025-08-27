using System.Text.Json;
using Azure.Messaging.ServiceBus;
using GamePlatform.Pagamentos.Application.DTOs.Messaging;
using GamePlatform.Pagamentos.Application.Interfaces.Services;
using GamePlatform.Pagamentos.Infrastructure.Messaging;
using Microsoft.Extensions.Options;

namespace GamePlatform.Pagamentos.Api.BackgroundServices;

public class PaymentProcessingResultBackgroundService : BackgroundService
{
    private readonly ServiceBusClient _client;
    private readonly ServiceBusOptions _options;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<PaymentProcessingResultBackgroundService> _logger;
    private ServiceBusProcessor? _processor;
    
    public PaymentProcessingResultBackgroundService(
        ServiceBusClient client,
        IOptions<ServiceBusOptions> options,
        IServiceScopeFactory scopeFactory,
        ILogger<PaymentProcessingResultBackgroundService> logger)
    {
        _client = client;
        _options = options.Value;
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (string.IsNullOrWhiteSpace(_options.PaymentProcessingResultQueue))
        {
            _logger.LogError("AzureServiceBus QueueName não configurado.");
            return;
        }

        var processorOptions = new ServiceBusProcessorOptions
        {
            MaxConcurrentCalls = _options.MaxConcurrentCalls,
            AutoCompleteMessages = false,
            PrefetchCount = _options.PrefetchCount
        };

        _processor = _client.CreateProcessor(_options.PaymentProcessingResultQueue, processorOptions);
        _processor.ProcessMessageAsync += OnMessageAsync;
        _processor.ProcessErrorAsync += OnErrorAsync;

        _logger.LogInformation("Iniciando o Service Bus Processor para a fila {Queue}", _options.PaymentProcessingResultQueue);
        await _processor.StartProcessingAsync(stoppingToken);

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
        catch (OperationCanceledException)
        {
                
        }
        finally
        {
            _logger.LogInformation("Parando o Service Bus Processor...");
            if (_processor is not null)
            {
                try
                {
                    await _processor.StopProcessingAsync(CancellationToken.None);
                }
                catch { }
                _processor.ProcessMessageAsync -= OnMessageAsync;
                _processor.ProcessErrorAsync -= OnErrorAsync;
                await _processor.DisposeAsync();
            }
        }
    }
    
    private async Task OnMessageAsync(ProcessMessageEventArgs args)
    {
        try
        {
            var body = args.Message.Body.ToString();
            var message = JsonSerializer.Deserialize<PaymentProcessingResultMessage>(body, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (message is null)
            {
                _logger.LogWarning("Falha ao desserializar mensagem. Abandonando MessageId={MessageId}", args.Message.MessageId);
                await args.AbandonMessageAsync(args.Message);
                return;
            }

            using var scope = _scopeFactory.CreateScope();
            var pagamentoService = scope.ServiceProvider.GetRequiredService<IPagamentoService>();
            await pagamentoService.AtualizarResultadoPagamentoAsync(message);

            await args.CompleteMessageAsync(args.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao processar a mensagem (MessageId={MessageId}). Abandonando.", args.Message.MessageId);
            try { await args.AbandonMessageAsync(args.Message); } catch {  }
        }
    }

    private Task OnErrorAsync(ProcessErrorEventArgs args)
    {
        _logger.LogError(args.Exception, "Erro no Service Bus Processor. Entity={Entity}, Namespace={Namespace}", args.EntityPath, args.FullyQualifiedNamespace);
        return Task.CompletedTask;
    }
}