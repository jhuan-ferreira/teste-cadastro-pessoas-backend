using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pessoas.API.Models;

public class Pessoa
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Nome { get; set; }

    public string? Sexo { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    public DateTime DataNascimento { get; set; }

    public string? Naturalidade { get; set; }

    public string? Nacionalidade { get; set; }

    [Required]
    [MaxLength(11)]
    public string Cpf { get; set; }

    public string? Endereco { get; set; }

    public DateTime DataCadastro { get; set; }
    public DateTime DataAtualizacao { get; set; }
}