using GamePlatform.Pagamentos.Domain.Enums;

namespace GamePlatform.Pagamentos.Application.DTOs.Pagamento;

public class PagamentoDto
{
    public Guid Id { get; set; }
    public Guid UsuarioId { get; set; }
    public Guid JogoId { get; set; }
    public string Status { get; set; }
    public decimal Valor { get; set; }
    public DateTime CreatedAt { get; set; }
}