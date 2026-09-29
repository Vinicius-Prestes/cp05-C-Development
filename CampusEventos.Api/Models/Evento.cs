using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampusEventos.Api.Models;

[Table("TB_EVENTOS")]
public class Evento
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(150)]
    public string Titulo { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Descricao { get; set; }

    [Required]
    public DateTime DataHora { get; set; }

    [Required]
    [MaxLength(200)]
    public string Local { get; set; } = string.Empty;

    [Range(1, 10000)]
    public int CapacidadeMaxima { get; set; }

    [Column(TypeName = "NUMBER(10,2)")]
    public decimal Preco { get; set; }

    // Chave estrangeira para Categoria
    public int CategoriaId { get; set; }

    [ForeignKey("CategoriaId")]
    public Categoria? Categoria { get; set; }
}
