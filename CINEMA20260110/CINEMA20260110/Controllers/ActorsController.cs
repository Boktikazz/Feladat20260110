using Mezei_Botond_backend.Data;
using Mezei_Botond_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class ActorsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private const string UID = "FKB3F4FEA09CE43C";

    public ActorsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("feladat9/{actorName}")]
    public async Task<IActionResult> GetActorWithMovies(string actorName)
    {
        var actor = await _context.Actors
            .Include(a => a.Movies)
            .ThenInclude(m => m.FilmType)
            .FirstOrDefaultAsync(a => a.ActorName == actorName);

        if (actor == null)
        {
            return NotFound(new { message = "Actor not found" });
        }

        return Ok(actor);
    }

    [HttpGet("feladat12")]
    public async Task<IActionResult> GetActorsCount()
    {
        try
        {
            var count = await _context.Actors.CountAsync();
            return Ok(new { count });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Error: {ex.Message}" });
        }
    }

    [HttpPost("feladat13")]
    public async Task<IActionResult> AddMovie([FromBody] Movie movie, [FromQuery] string uid)
    {
        try
        {
            if (uid != UID)
            {
                return Unauthorized(new { message = "Nincs jogosultsága új film felvételéhez!" });
            }

            
            if (movie == null)
            {
                return BadRequest(new { message = "Invalid movie data" });
            }

            movie.MovieId = 0;

            var actorExists = await _context.Actors.AnyAsync(a => a.ActorId == movie.ActorId);
            if (!actorExists)
            {
                return BadRequest(new { message = "Actor does not exist" });
            }

            var filmTypeExists = await _context.FilmTypes.AnyAsync(f => f.TypeId == movie.FilmTypeId);
            if (!filmTypeExists)
            {
                return BadRequest(new { message = "Film type does not exist" });
            }

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return StatusCode(201, new { message = "Film hozzáadása sikeresen megtörtént." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Error: {ex.Message}" });
        }
    }
}