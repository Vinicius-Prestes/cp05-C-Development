using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampusEventos.Api.Models;

[Table("TB_CATEGORIAS")]
public class Categoria
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Nome { get; set; } = string.Empty;

    [MaxLength(250)]
    public string? Descricao { get; set; }

    public bool Ativo { get; set; } = true;

    // Relacionamento 1:N com Eventos
    public ICollection<Evento> Eventos { get; set; } = new List<Evento>();
}
