namespace GamePlatform.Pagamentos.Domain.Entities;

public class Pagamento : BaseEntity
{
    public Pagamento(Guid usuarioId, Guid jogoId, decimal valor, string status)
    {
        UsuarioId = usuarioId;
        JogoId = jogoId;
        Valor = valor;
        Status = status;
    }
    
    public Guid UsuarioId { get; private set; }
    public Guid JogoId { get; private set; }
    public decimal Valor { get; private set; }
    public string Status { get; private set; }
}