using GamePlatform.Pagamentos.Application.DTOs;

namespace GamePlatform.Pagamentos.Application.Interfaces.Services;

public interface IPagamentoService
{
    public Task<BaseResponseDto> ObterPorIdAsync(Guid id);
}