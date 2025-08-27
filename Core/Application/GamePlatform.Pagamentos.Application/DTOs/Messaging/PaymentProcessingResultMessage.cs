namespace GamePlatform.Pagamentos.Application.DTOs.Messaging;

public class PaymentProcessingResultMessage
{
    public Guid PagamentoId { get; init; }
    public bool Sucesso { get; init; }
}