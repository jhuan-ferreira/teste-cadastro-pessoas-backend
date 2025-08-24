using FluentValidation;
using Pessoas.API.DTOs.V1;

namespace Pessoas.API.DTOs.V2;

public class PessoaCreateDtoV2 : PessoaCreateDto
{
    public string Endereco { get; set; }
}

public class PessoaCreateDtoV2Validator : AbstractValidator<PessoaCreateDtoV2>
{
    public PessoaCreateDtoV2Validator()
    {
        Include(new PessoaCreateDtoValidator());
        RuleFor(p => p.Endereco).NotEmpty().MinimumLength(10);
    }
}