﻿namespace SmartOficina.Api.Domain.Model;

public class UserAutentication : IdentityUser
{
    public DateTime DataCadastro { get; set; }
    public DateTime? DataDesativacao { get; set; }
    public Guid UsrCadastro { get; set; }
    public string UsrDescricaoCadastro { get; set; }
    public Guid? UsrDesativacao { get; set; }
    public string UsrDescricaoDesativacao { get; set; }
}
