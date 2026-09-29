using System.ComponentModel.DataAnnotations;

namespace CampusEventos.Api.DTOs;

public class EventoDto
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public DateTime DataHora { get; set; }
    public string Local { get; set; } = string.Empty;
    public int CapacidadeMaxima { get; set; }
    public decimal Preco { get; set; }
    public int CategoriaId { get; set; }
    public string? CategoriaNome { get; set; }
}

public class CriarEventoDto
{
    [Required(ErrorMessage = "O título do evento é obrigatório.")]
    [StringLength(150, MinimumLength = 3, ErrorMessage = "O título deve ter entre 3 e 150 caracteres.")]
    public string Titulo { get; set; } = string.Empty;

    [MaxLength(500, ErrorMessage = "A descrição não pode ultrapassar 500 caracteres.")]
    public string? Descricao { get; set; }

    [Required(ErrorMessage = "A data e hora do evento são obrigatórias.")]
    public DateTime DataHora { get; set; }

    [Required(ErrorMessage = "O local do evento é obrigatório.")]
    [MaxLength(200, ErrorMessage = "O local não pode ultrapassar 200 caracteres.")]
    public string Local { get; set; } = string.Empty;

    [Range(1, 10000, ErrorMessage = "A capacidade máxima deve ser de pelo menos 1 participante.")]
    public int CapacidadeMaxima { get; set; }

    [Range(0, 99999.99, ErrorMessage = "O preço deve ser um valor positivo.")]
    public decimal Preco { get; set; }

    [Required(ErrorMessage = "O ID da categoria é obrigatório.")]
    [Range(1, int.MaxValue, ErrorMessage = "Informe um ID de categoria válido.")]
    public int CategoriaId { get; set; }
}

public class AtualizarEventoDto
{
    [Required(ErrorMessage = "O título do evento é obrigatório.")]
    [StringLength(150, MinimumLength = 3, ErrorMessage = "O título deve ter entre 3 e 150 caracteres.")]
    public string Titulo { get; set; } = string.Empty;

    [MaxLength(500, ErrorMessage = "A descrição não pode ultrapassar 500 caracteres.")]
    public string? Descricao { get; set; }

    [Required(ErrorMessage = "A data e hora do evento são obrigatórias.")]
    public DateTime DataHora { get; set; }

    [Required(ErrorMessage = "O local do evento é obrigatório.")]
    [MaxLength(200, ErrorMessage = "O local não pode ultrapassar 200 caracteres.")]
    public string Local { get; set; } = string.Empty;

    [Range(1, 10000, ErrorMessage = "A capacidade máxima deve ser de pelo menos 1 participante.")]
    public int CapacidadeMaxima { get; set; }

    [Range(0, 99999.99, ErrorMessage = "O preço deve ser um valor positivo.")]
    public decimal Preco { get; set; }

    [Required(ErrorMessage = "O ID da categoria é obrigatório.")]
    [Range(1, int.MaxValue, ErrorMessage = "Informe um ID de categoria válido.")]
    public int CategoriaId { get; set; }
}
