﻿namespace SmartOficina.Api.Infrastructure.Configuration;

public class ServicoConfiguration : IEntityTypeConfiguration<Servico>
{
    public void Configure(EntityTypeBuilder<Servico> builder)
    {
        builder.ToTable(nameof(Servico));

        builder.HasKey(k => k.Id);

        builder.Property(p => p.Id).HasValueGenerator<SequentialGuidValueGenerator>();

        builder.Property(p => p.Nome).IsRequired().HasMaxLength(250);

        builder.Property(p => p.Valor).IsRequired();
    }
}
