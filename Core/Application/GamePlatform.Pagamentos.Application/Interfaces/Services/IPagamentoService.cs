using GamePlatform.Pagamentos.Application.DTOs;
using GamePlatform.Pagamentos.Application.DTOs.Messaging;

namespace GamePlatform.Pagamentos.Application.Interfaces.Services;

public interface IPagamentoService
{
    public Task<BaseResponseDto> ObterPorIdAsync(Guid id);
    public Task ProcessarPagamentoAsync(GamePurchaseRequestedMessage message);
    public Task AtualizarResultadoPagamentoAsync(PaymentProcessingResultMessage message);
}