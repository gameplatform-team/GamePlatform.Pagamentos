using GamePlatform.Pagamentos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamePlatform.Pagamentos.Infrastructure.Data.Configuration;

public class PagamentoConfiguration : IEntityTypeConfiguration<Pagamento>
{
    public void Configure(EntityTypeBuilder<Pagamento> builder)
    {
        builder.ToTable("Pagamentos");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.UsuarioId)
            .IsRequired();

        builder.Property(p => p.JogoId)
            .IsRequired();
        
        builder.Property(p => p.Valor)
            .IsRequired()
            .HasColumnType("decimal(18,2)");
        
        builder.Property(p => p.Status)
            .IsRequired();
    }
}