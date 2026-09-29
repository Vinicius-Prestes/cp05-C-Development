using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CampusEventos.Api.Data;
using CampusEventos.Api.DTOs;
using CampusEventos.Api.Models;

namespace CampusEventos.Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class EventosController : ControllerBase
{
    private readonly AppDbContext _context;

    public EventosController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/v1/eventos?termo=workshop&categoriaId=1
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<EventoDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<EventoDto>>> ObterTodos(
        [FromQuery] string? termo = null,
        [FromQuery] int? categoriaId = null)
    {
        var query = _context.Eventos
            .Include(e => e.Categoria)
            .AsNoTracking()
            .AsQueryable();

        // Filtro opcional por termo no título ou descrição
        if (!string.IsNullOrWhiteSpace(termo))
        {
            var termoNormalizado = termo.Trim().ToLower();
            query = query.Where(e =>
                e.Titulo.ToLower().Contains(termoNormalizado) ||
                (e.Descricao != null && e.Descricao.ToLower().Contains(termoNormalizado)));
        }

        // Filtro opcional por categoria
        if (categoriaId.HasValue)
        {
            query = query.Where(e => e.CategoriaId == categoriaId.Value);
        }

        var eventos = await query
            .OrderBy(e => e.DataHora)
            .Select(e => new EventoDto
            {
                Id = e.Id,
                Titulo = e.Titulo,
                Descricao = e.Descricao,
                DataHora = e.DataHora,
                Local = e.Local,
                CapacidadeMaxima = e.CapacidadeMaxima,
                Preco = e.Preco,
                CategoriaId = e.CategoriaId,
                CategoriaNome = e.Categoria != null ? e.Categoria.Nome : null
            })
            .ToListAsync();

        return Ok(eventos);
    }

    // GET: api/v1/eventos/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(EventoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EventoDto>> ObterPorId(int id)
    {
        var evento = await _context.Eventos
            .Include(e => e.Categoria)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);

        if (evento == null)
        {
            return NotFound(new { mensagem = $"Evento com ID {id} não foi encontrado." });
        }

        var dto = new EventoDto
        {
            Id = evento.Id,
            Titulo = evento.Titulo,
            Descricao = evento.Descricao,
            DataHora = evento.DataHora,
            Local = evento.Local,
            CapacidadeMaxima = evento.CapacidadeMaxima,
            Preco = evento.Preco,
            CategoriaId = evento.CategoriaId,
            CategoriaNome = evento.Categoria?.Nome
        };

        return Ok(dto);
    }

    // POST: api/v1/eventos
    [HttpPost]
    [ProducesResponseType(typeof(EventoDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EventoDto>> Criar([FromBody] CriarEventoDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Valida se a categoria informada existe
        var categoriaExiste = await _context.Categorias.AnyAsync(c => c.Id == dto.CategoriaId);
        if (!categoriaExiste)
        {
            return BadRequest(new { mensagem = $"Não existe categoria com o ID {dto.CategoriaId} informado." });
        }

        var evento = new Evento
        {
            Titulo = dto.Titulo,
            Descricao = dto.Descricao,
            DataHora = dto.DataHora,
            Local = dto.Local,
            CapacidadeMaxima = dto.CapacidadeMaxima,
            Preco = dto.Preco,
            CategoriaId = dto.CategoriaId
        };

        _context.Eventos.Add(evento);
        await _context.SaveChangesAsync();

        // Carrega os dados da categoria para retornar completo no DTO
        var categoria = await _context.Categorias.FindAsync(evento.CategoriaId);

        var eventoCriado = new EventoDto
        {
            Id = evento.Id,
            Titulo = evento.Titulo,
            Descricao = evento.Descricao,
            DataHora = evento.DataHora,
            Local = evento.Local,
            CapacidadeMaxima = evento.CapacidadeMaxima,
            Preco = evento.Preco,
            CategoriaId = evento.CategoriaId,
            CategoriaNome = categoria?.Nome
        };

        return CreatedAtAction(
            nameof(ObterPorId),
            new { id = evento.Id, version = "1.0" },
            eventoCriado
        );
    }

    // PUT: api/v1/eventos/5
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(EventoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EventoDto>> Atualizar(int id, [FromBody] AtualizarEventoDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var evento = await _context.Eventos.FindAsync(id);
        if (evento == null)
        {
            return NotFound(new { mensagem = $"Evento com ID {id} não foi encontrado." });
        }

        // Valida se a categoria existe
        var categoriaExiste = await _context.Categorias.AnyAsync(c => c.Id == dto.CategoriaId);
        if (!categoriaExiste)
        {
            return BadRequest(new { mensagem = $"Não existe categoria com o ID {dto.CategoriaId} informado." });
        }

        evento.Titulo = dto.Titulo;
        evento.Descricao = dto.Descricao;
        evento.DataHora = dto.DataHora;
        evento.Local = dto.Local;
        evento.CapacidadeMaxima = dto.CapacidadeMaxima;
        evento.Preco = dto.Preco;
        evento.CategoriaId = dto.CategoriaId;

        await _context.SaveChangesAsync();

        var categoria = await _context.Categorias.FindAsync(evento.CategoriaId);

        var eventoAtualizado = new EventoDto
        {
            Id = evento.Id,
            Titulo = evento.Titulo,
            Descricao = evento.Descricao,
            DataHora = evento.DataHora,
            Local = evento.Local,
            CapacidadeMaxima = evento.CapacidadeMaxima,
            Preco = evento.Preco,
            CategoriaId = evento.CategoriaId,
            CategoriaNome = categoria?.Nome
        };

        return Ok(eventoAtualizado);
    }

    // DELETE: api/v1/eventos/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Excluir(int id)
    {
        var evento = await _context.Eventos.FindAsync(id);
        if (evento == null)
        {
            return NotFound(new { mensagem = $"Evento com ID {id} não foi encontrado." });
        }

        _context.Eventos.Remove(evento);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
