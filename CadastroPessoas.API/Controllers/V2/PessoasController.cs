using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pessoas.API.Data;
using Pessoas.API.DTOs.V2;
using Pessoas.API.Models;

namespace Pessoas.API.Controllers.V2;

[ApiController]
[Route("api/v{version:apiVersion}/pessoas")]
[ApiVersion("2.0")]
[Authorize]
public class PessoasController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PessoasController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PessoaCreateDtoV2 pessoaDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var pessoa = new Pessoa
        {
            Nome = pessoaDto.Nome,
            Sexo = pessoaDto.Sexo,
            Email = pessoaDto.Email,
            DataNascimento = pessoaDto.DataNascimento,
            Naturalidade = pessoaDto.Naturalidade,
            Nacionalidade = pessoaDto.Nacionalidade,
            Cpf = new string(pessoaDto.Cpf.Where(char.IsDigit).ToArray()),
            Endereco = pessoaDto.Endereco,
            DataCadastro = DateTime.UtcNow,
            DataAtualizacao = DateTime.UtcNow
        };

        _context.Pessoas.Add(pessoa);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(V1.PessoasController.GetById),
            "Pessoas",
            new { id = pessoa.Id, version = "1.0" },
            pessoa
        );
    }

}