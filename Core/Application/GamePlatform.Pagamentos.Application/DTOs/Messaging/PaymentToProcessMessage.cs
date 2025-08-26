namespace GamePlatform.Pagamentos.Application.DTOs.Messaging;

public class PaymentToProcessMessage
{
    public Guid PagamentoId { get; set; }
    public decimal Valor { get; set; }
}