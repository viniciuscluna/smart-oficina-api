﻿namespace SmartOficina.Api.Dto
{
    public class FuncionarioPrestadorDto
    {
        //public int Id_prestador { get; set; }
        //public required Guid PrestadorId { get; set; }
        public required string Nome { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
        public string RG { get; set; }
        public string CPF { get; set; }
        public string Endereco { get; set; }
        public string Cargo { get; set; }
    }
}