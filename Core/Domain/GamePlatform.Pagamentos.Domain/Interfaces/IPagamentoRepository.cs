using GamePlatform.Pagamentos.Domain.Entities;

namespace GamePlatform.Pagamentos.Domain.Interfaces;

public interface IPagamentoRepository
{
    public Task<Pagamento?> ObterPorIdAsync(Guid id);
}