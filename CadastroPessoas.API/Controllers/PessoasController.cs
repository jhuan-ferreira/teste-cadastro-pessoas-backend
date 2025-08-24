using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pessoas.API.Data;
using Pessoas.API.DTOs.V1;
using Pessoas.API.Models;

namespace Pessoas.API.Controllers.V1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[Authorize] 
public class PessoasController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PessoasController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var pessoas = await _context.Pessoas.ToListAsync();
        return Ok(pessoas);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var pessoa = await _context.Pessoas.FindAsync(id);
        if (pessoa == null)
        {
            return NotFound();
        }
        return Ok(pessoa);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PessoaCreateDto pessoaDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (await _context.Pessoas.AnyAsync(p => p.Cpf == pessoaDto.Cpf))
        {
            return Conflict("CPF já cadastrado.");
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
            DataCadastro = DateTime.UtcNow,
            DataAtualizacao = DateTime.UtcNow
        };

        _context.Pessoas.Add(pessoa);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = pessoa.Id, version = "1.0" }, pessoa);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] PessoaUpdateDto pessoaDto)
    {
        // Não tive tempo de desenvolver, mas basicamente, aqui seria a lógica para realizar o Update do cadastro de pessoa. 
        
        return NoContent(); 
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var pessoa = await _context.Pessoas.FindAsync(id);
        if (pessoa == null)
        {
            return NotFound();
        }
        _context.Pessoas.Remove(pessoa);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}