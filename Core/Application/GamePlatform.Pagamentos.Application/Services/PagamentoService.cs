using GamePlatform.Pagamentos.Application.DTOs;
using GamePlatform.Pagamentos.Application.DTOs.Messaging;
using GamePlatform.Pagamentos.Application.DTOs.Pagamento;
using GamePlatform.Pagamentos.Application.Interfaces.Services;
using GamePlatform.Pagamentos.Domain.Entities;
using GamePlatform.Pagamentos.Domain.Enums;
using GamePlatform.Pagamentos.Domain.Interfaces;
using GamePlatform.Pagamentos.Domain.Interfaces.Messaging;
using Microsoft.Extensions.Logging;

namespace GamePlatform.Pagamentos.Application.Services;

public class PagamentoService : IPagamentoService
{
    private readonly IPagamentoRepository _pagamentoRepository;
    private readonly ILogger<PagamentoService> _logger;
    private readonly IServiceBusPublisher _publisher;

    public PagamentoService(
        IPagamentoRepository pagamentoRepository,
        ILogger<PagamentoService> logger,
        IServiceBusPublisher publisher)
    {
        _pagamentoRepository = pagamentoRepository;
        _logger = logger;
        _publisher = publisher;
    }

    public async Task<BaseResponseDto> ObterPorIdAsync(Guid id)
    {
        var pagamento = await _pagamentoRepository.ObterPorIdAsync(id);
        
        if (pagamento == null)
            return new BaseResponseDto(false, "Pagamento não encontrado");

        var pagamentoDto = new PagamentoDto
        {
            Id = pagamento.Id,
            UsuarioId = pagamento.UsuarioId,
            JogoId = pagamento.JogoId,
            Status = pagamento.Status,
            Valor = pagamento.Valor,
            CreatedAt = pagamento.CreatedAt
        };
        
        return new DataResponseDto<PagamentoDto>(true, string.Empty, pagamentoDto);
    }

    public async Task ProcessarPagamentoAsync(GamePurchaseRequestedMessage message)
    {
        var pagamento = new Pagamento(message.UsuarioId, message.JogoId, message.Preco, nameof(StatusPagamento.Pendente));
        
        await _pagamentoRepository.AdicionarAsync(pagamento);
        
        var processPaymentMessage = new PaymentToProcessMessage
        {
            PagamentoId = pagamento.Id,
            Valor = pagamento.Valor
        };
        
        await _publisher.PublishAsync(
            queueName: "payment-to-process",
            message: processPaymentMessage,
            messageId: Guid.NewGuid().ToString(),
            correlationId: pagamento.Id.ToString(),
            ct: CancellationToken.None
        );
    }
}