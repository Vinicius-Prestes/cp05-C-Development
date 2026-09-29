using System.ComponentModel.DataAnnotations;

namespace CampusEventos.Api.DTOs;

public class CategoriaDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public bool Ativo { get; set; }
    public int TotalEventos { get; set; }
}

public class CriarCategoriaDto
{
    [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [MaxLength(250, ErrorMessage = "A descrição não pode ultrapassar 250 caracteres.")]
    public string? Descricao { get; set; }
}

public class AtualizarCategoriaDto
{
    [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [MaxLength(250, ErrorMessage = "A descrição não pode ultrapassar 250 caracteres.")]
    public string? Descricao { get; set; }

    public bool Ativo { get; set; } = true;
}
