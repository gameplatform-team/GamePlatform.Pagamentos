using GamePlatform.Pagamentos.Domain.Entities;
using GamePlatform.Pagamentos.Domain.Enums;

namespace GamePlatform.Pagamentos.Tests.Core.Domain.Entities;

public class PagamentoTests
{
    [Fact]
    public void Deve_Criar_Pagamento_Com_Dados_Corretos()
    {
        // Arrange
        var usuarioId = Guid.NewGuid();
        var jogoId = Guid.NewGuid();
        var valor = 59.99m;
        var status = nameof(StatusPagamento.Pendente);

        // Act
        var pagamento = new Pagamento(usuarioId, jogoId, valor, status);

        // Assert
        Assert.Equal(usuarioId, pagamento.UsuarioId);
        Assert.Equal(jogoId, pagamento.JogoId);
        Assert.Equal(valor, pagamento.Valor);
        Assert.Equal(status, pagamento.Status);
        
        Assert.NotEqual(Guid.Empty, pagamento.Id);
        Assert.NotEqual(default, pagamento.CreatedAt);
        Assert.True(pagamento.CreatedAt <= DateTime.UtcNow);
        Assert.Null(pagamento.UpdatedAt);
    }
}