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
public class CategoriasController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoriasController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/v1/categorias
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CategoriaDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CategoriaDto>>> ObterTodos()
    {
        var categorias = await _context.Categorias
            .Include(c => c.Eventos)
            .AsNoTracking()
            .Select(c => new CategoriaDto
            {
                Id = c.Id,
                Nome = c.Nome,
                Descricao = c.Descricao,
                Ativo = c.Ativo,
                TotalEventos = c.Eventos.Count
            })
            .ToListAsync();

        return Ok(categorias);
    }

    // GET: api/v1/categorias/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(CategoriaDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoriaDto>> ObterPorId(int id)
    {
        var categoria = await _context.Categorias
            .Include(c => c.Eventos)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);

        if (categoria == null)
        {
            return NotFound(new { mensagem = $"Categoria com ID {id} não foi encontrada." });
        }

        var dto = new CategoriaDto
        {
            Id = categoria.Id,
            Nome = categoria.Nome,
            Descricao = categoria.Descricao,
            Ativo = categoria.Ativo,
            TotalEventos = categoria.Eventos.Count
        };

        return Ok(dto);
    }

    // POST: api/v1/categorias
    [HttpPost]
    [ProducesResponseType(typeof(CategoriaDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CategoriaDto>> Criar([FromBody] CriarCategoriaDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var categoria = new Categoria
        {
            Nome = dto.Nome,
            Descricao = dto.Descricao,
            Ativo = true
        };

        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();

        var categoriaCriada = new CategoriaDto
        {
            Id = categoria.Id,
            Nome = categoria.Nome,
            Descricao = categoria.Descricao,
            Ativo = categoria.Ativo,
            TotalEventos = 0
        };

        return CreatedAtAction(
            nameof(ObterPorId),
            new { id = categoria.Id, version = "1.0" },
            categoriaCriada
        );
    }

    // PUT: api/v1/categorias/5
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(CategoriaDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoriaDto>> Atualizar(int id, [FromBody] AtualizarCategoriaDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var categoria = await _context.Categorias.FindAsync(id);
        if (categoria == null)
        {
            return NotFound(new { mensagem = $"Categoria com ID {id} não foi encontrada." });
        }

        categoria.Nome = dto.Nome;
        categoria.Descricao = dto.Descricao;
        categoria.Ativo = dto.Ativo;

        await _context.SaveChangesAsync();

        var categoriaAtualizada = new CategoriaDto
        {
            Id = categoria.Id,
            Nome = categoria.Nome,
            Descricao = categoria.Descricao,
            Ativo = categoria.Ativo,
            TotalEventos = await _context.Eventos.CountAsync(e => e.CategoriaId == id)
        };

        return Ok(categoriaAtualizada);
    }

    // DELETE: api/v1/categorias/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Excluir(int id)
    {
        var categoria = await _context.Categorias
            .Include(c => c.Eventos)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (categoria == null)
        {
            return NotFound(new { mensagem = $"Categoria com ID {id} não foi encontrada." });
        }

        if (categoria.Eventos.Any())
        {
            return BadRequest(new
            {
                mensagem = "Não é permitido excluir uma categoria que possui eventos cadastrados. Remova ou altere os eventos primeiro."
            });
        }

        _context.Categorias.Remove(categoria);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
