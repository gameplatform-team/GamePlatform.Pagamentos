namespace GamePlatform.Pagamentos.Application.DTOs.Messaging;

public class GamePurchaseRequestedMessage
{
    public string TipoEvento { get; set; }
    public Guid UsuarioId { get; set; }
    public Guid JogoId { get; set; }
    public decimal Preco { get; set; }
    public DateTime SolicitadoEm { get; set; }
}