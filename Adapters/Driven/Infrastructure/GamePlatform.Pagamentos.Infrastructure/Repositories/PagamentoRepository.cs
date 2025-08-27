using GamePlatform.Pagamentos.Domain.Entities;
using GamePlatform.Pagamentos.Domain.Interfaces;
using GamePlatform.Pagamentos.Infrastructure.Data;

namespace GamePlatform.Pagamentos.Infrastructure.Repositories;

public class PagamentoRepository : IPagamentoRepository
{
    private readonly DataContext _context;
    
    public PagamentoRepository(DataContext context)
    {
        _context = context;
    }
    
    public async Task<Pagamento?> ObterPorIdAsync(Guid id)
    {
        return await _context.Pagamentos.FindAsync(id);
    }

    public async Task AdicionarAsync(Pagamento pagamento)
    {
        await _context.Pagamentos.AddAsync(pagamento);
        await SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}