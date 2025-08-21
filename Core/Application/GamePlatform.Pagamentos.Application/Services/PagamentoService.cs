using GamePlatform.Pagamentos.Application.DTOs;
using GamePlatform.Pagamentos.Application.DTOs.Pagamento;
using GamePlatform.Pagamentos.Application.Interfaces.Services;
using GamePlatform.Pagamentos.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace GamePlatform.Pagamentos.Application.Services;

public class PagamentoService : IPagamentoService
{
    private readonly IPagamentoRepository _pagamentoRepository;
    private readonly ILogger<PagamentoService> _logger;

    public PagamentoService(IPagamentoRepository pagamentoRepository, ILogger<PagamentoService> logger)
    {
        _pagamentoRepository = pagamentoRepository;
        _logger = logger;
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
}