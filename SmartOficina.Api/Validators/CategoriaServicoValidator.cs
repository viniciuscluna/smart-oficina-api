﻿namespace SmartOficina.Api.Validators;

public class CategoriaServicoValidator : AbstractValidator<CategoriaServicoDto>
{
    public CategoriaServicoValidator()
    {
        RuleFor(x => x.Desc)
            .NotEmpty()
            .WithMessage(CategoriaConst.DescricaoValidation)
            .NotNull()
            .WithMessage(CategoriaConst.DescricaoValidation);

        RuleFor(x => x.Titulo)
            .NotEmpty()
            .WithMessage(CategoriaConst.TituloValidation)
            .NotNull()
            .WithMessage(CategoriaConst.TituloValidation);
    }
}
