﻿namespace SmartOficina.Api.Infrastructure.Configurations.ContextConfiguration;

public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable(nameof(Produto));

        builder.HasKey(k => k.Id);

        builder.Property(p => p.Nome).HasMaxLength(100).IsRequired();

        builder.Property(p => p.Marca).HasMaxLength(25).IsRequired();

        builder.Property(p => p.Modelo).HasMaxLength(50).IsRequired();

        builder.Property(p => p.Data_validade).HasDefaultValueSql("getDate()").IsRequired();

        builder.Property(p => p.Garantia).HasMaxLength(10).IsRequired();

        builder.Property(p => p.Valor_Compra).IsRequired();

        builder.Property(p => p.Valor_Venda).IsRequired();

        builder.Property(p => p.PrestadorId).IsRequired();

        builder.Property(p => p.UsrCadastro).IsRequired();

        builder.Property(p => p.DataCadastro).HasDefaultValueSql("getDate()").IsRequired();
    }
}
