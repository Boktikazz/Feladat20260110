using Mezei_Botond_backend.Data;
using Mezei_Botond_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mezei_Botond_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmTypesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FilmTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("feladat11")]
        public async Task<ActionResult<IEnumerable<FilmType>>> GetFilmTypesWithMovies()
        {
            try
            {
                var filmTypes = await _context.FilmTypes
                    .Include(ft => ft.Movies)
                    .ThenInclude(m => m.Actor)
                    .ToListAsync();

                if (filmTypes == null || !filmTypes.Any())
                {
                    return NotFound(new { message = "No film types found" });
                }

                return Ok(filmTypes);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error: {ex.Message}" });
            }
        }
    }
}