using ApiMusicBox.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace AspNetCore.WebAPI.Controllers
{
	[ApiController]
	[Route("api/Genres")]
	public class GenresController : ControllerBase
	{
		private readonly SongContext _context;

		public GenresController(SongContext context)
		{
			_context = context;
		}

		// GET: api/Songs
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Genres>>> GetGenres()
		{
			return await _context.genres.ToListAsync();
		}

		// GET: api/Songs/3
		[HttpGet("{id}")]
		public async Task<ActionResult<Genres>> GetGenre(int id)
		{
			var genre = await _context.genres.SingleOrDefaultAsync(m => m.Id == id);
			if (genre == null)
			{
				return NotFound();
			}
			return new ObjectResult(genre);
		}

		// PUT: api/Songs
		[HttpPut]
		public async Task<ActionResult<Genres>> PutGenre(Genres genre)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			if (!_context.genres.Any(e => e.Id == genre.Id))
			{
				return NotFound();
			}

			_context.Update(genre);
			await _context.SaveChangesAsync();
			return Ok(genre);
		}

		// POST: api/Songs
		[HttpPost]
		public async Task<ActionResult<Genres>> PostGenre(Genres g)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			_context.genres.Add(g);
			await _context.SaveChangesAsync();

			return Ok(g);
		}

		// DELETE: api/Songs/3
		[HttpDelete("{id}")]
		public async Task<ActionResult<Genres>> DeleteGenre(int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var genre = await _context.genres.SingleOrDefaultAsync(m => m.Id == id);
			if (genre == null)
			{
				return NotFound();
			}

			_context.genres.Remove(genre);
			await _context.SaveChangesAsync();

			return Ok(genre);
		}
	}
}
