﻿namespace SmartOficina.Api.Domain.Model;

public class Veiculo : Base
{
    public Veiculo()
    {

    }
    public required string Placa { get; set; }
    public required string Marca { get; set; }
    public required string Modelo { get; set; }
    public string? Chassi { get; set; }
    public EVeiculoTipo Tipo { get; set; }
    public Guid PrestadorId { get; set; }

    public ICollection<PrestacaoServico>? Servicos { get; set; }
}
