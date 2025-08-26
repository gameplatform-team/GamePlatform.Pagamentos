using GamePlatform.Pagamentos.Application.DTOs;
using GamePlatform.Pagamentos.Application.DTOs.Pagamento;
using GamePlatform.Pagamentos.Application.Services;
using GamePlatform.Pagamentos.Domain.Entities;
using GamePlatform.Pagamentos.Domain.Enums;
using GamePlatform.Pagamentos.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;

namespace GamePlatform.Pagamentos.Tests.Core.Application.Services;

public class PagamentoServiceTests
{
    private readonly Mock<IPagamentoRepository> _pagamentoRepoMock = new();
    private readonly Mock<ILogger<PagamentoService>> _loggerMock = new();
    private readonly PagamentoService _pagamentoService;
    
    public PagamentoServiceTests()
    {
        _pagamentoService = new PagamentoService(_pagamentoRepoMock.Object, _loggerMock.Object);
    }
    
    [Fact]
    public async Task ObterPorIdAsync_DeveRetornarPagamento_QuandoExistirAsync()
    {
        // Arrange
        var pagamento = new Pagamento(Guid.NewGuid(), Guid.NewGuid(), 99.99m, nameof(StatusPagamento.Pendente));
        var pagamentoDto = new PagamentoDto
        {
            Id = pagamento.Id,
            UsuarioId = pagamento.UsuarioId,
            JogoId = pagamento.JogoId,
            Valor = pagamento.Valor,
            Status = pagamento.Status,
            CreatedAt = pagamento.CreatedAt
        };
        
        _pagamentoRepoMock.Setup(x => x.ObterPorIdAsync(It.IsAny<Guid>())).ReturnsAsync(pagamento);
        
        // Act
        var resultado = await _pagamentoService.ObterPorIdAsync(Guid.NewGuid()) as DataResponseDto<PagamentoDto>;
        
        // Assert
        _pagamentoRepoMock.Verify(x => x.ObterPorIdAsync(It.IsAny<Guid>()), Times.Once);
        Assert.Equivalent(pagamentoDto, resultado!.Data, true);
        Assert.True(resultado.Sucesso);
    }

    [Fact]
    public async Task ObterPorIdAsync_DeveRetornarErro_QuandoNaoExistirAsync()
    {
        // Arrange
        _pagamentoRepoMock.Setup(x => x.ObterPorIdAsync(It.IsAny<Guid>())).ReturnsAsync((Pagamento?)null);
        
        // Act
        var resultado = await _pagamentoService.ObterPorIdAsync(Guid.NewGuid());

        // Assert
        _pagamentoRepoMock.Verify(x => x.ObterPorIdAsync(It.IsAny<Guid>()), Times.Once);
        Assert.False(resultado.Sucesso);
        Assert.Equal("Pagamento não encontrado", resultado.Mensagem);
    }
}