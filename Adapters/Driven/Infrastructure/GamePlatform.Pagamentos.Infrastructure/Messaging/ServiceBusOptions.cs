namespace GamePlatform.Pagamentos.Infrastructure.Messaging;

public class ServiceBusOptions
{
    public string ConnectionString { get; set; } = string.Empty;
    public string GamePurchaseRequestedQueue { get; set; } = "game-purchase-requested";
    public string PaymentProcessingResultQueue { get; set; } = "payment-result";
    public int MaxConcurrentCalls { get; set; } = 1;
    public int PrefetchCount { get; set; } = 0;
}