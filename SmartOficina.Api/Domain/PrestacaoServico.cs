﻿namespace SmartOficina.Api.Domain
{
    public class PrestacaoServico : Base
    {
        public string? Referencia { get; set; }
        public PrestacaoServicoStatus Status { get; set; }
        public Prestador? Prestador { get; set; }
        public required Guid PrestadorId { get; set; }

        public Cliente? Cliente { get; set; }
        public required Guid ClienteId { get; set; }

        public Veiculo? Veiculo { get; set; }
        public required Guid VeiculoId { get; set; }

        public ICollection<Servico>? Servicos { get; set; }

    }
}
