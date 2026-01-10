using Mezei_Botond_backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public MoviesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("feladat10")]
    public async Task<IActionResult> GetAllMovies()
    {
        try
        {
            var movies = await _context.Movies
                .Include(m => m.Actor)
                .Include(m => m.FilmType)
                .ToListAsync();

            return Ok(movies);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Error: {ex.Message}" });
        }
    }
}
