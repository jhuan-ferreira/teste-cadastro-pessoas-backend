using FluentValidation;

namespace Pessoas.API.DTOs.V1;

public class PessoaUpdateDto
{
    public string Nome { get; set; }
    public string? Sexo { get; set; }
    public string? Email { get; set; }
    public DateTime DataNascimento { get; set; }
    public string? Naturalidade { get; set; }
    public string? Nacionalidade { get; set; }
    public string Cpf { get; set; }
}

public class PessoaUpdateDtoValidator : AbstractValidator<PessoaCreateDto>
{
    public PessoaUpdateDtoValidator()
    {
        RuleFor(p => p.Nome).MaximumLength(200);
        RuleFor(p => p.Email).EmailAddress().When(p => !string.IsNullOrEmpty(p.Email));
        RuleFor(p => p.DataNascimento).LessThan(DateTime.Now);
        RuleFor(p => p.Cpf).IsCpf();
    }
}