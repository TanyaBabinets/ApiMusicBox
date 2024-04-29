using ApiMusicBox.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace AspNetCore.WebAPI.Controllers
{
	[ApiController]
	[Route("api/User")]
	public class UserController : ControllerBase
	{
		private readonly SongContext _context;

		public UserController(SongContext context)
		{
			_context = context;
		}

		// GET: api/Songs
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
		{
			return await _context.users.ToListAsync();
		}

		// GET: api/Songs/3
		[HttpGet("{id}")]
		public async Task<ActionResult<Users>> GetUser(int id)
		{
			var user = await _context.users.FindAsync(id);
			if (user == null)
			{
				return NotFound();
			}
			return new ObjectResult(user);
		}

		// PUT: api/Songs
		[HttpPut]
		public async Task<ActionResult<Users>> PutUser(Users user)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			if (!_context.users.Any(e => e.Id == user.Id))
			{
				return NotFound();
			}

			_context.Update(user);
			await _context.SaveChangesAsync();
			return Ok(user);
		}

		// POST: api/Songs
		[HttpPost]
		public async Task<ActionResult<Users>> PostUser(Users g)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			_context.users.Add(g);
			await _context.SaveChangesAsync();

			return Ok(g);
		}

		// DELETE: api/Songs/3
		[HttpDelete("{id}")]
		public async Task<ActionResult<Users>> DeleteUser(int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var user = await _context.users.SingleOrDefaultAsync(m => m.Id == id);
			if (user == null)
			{
				return NotFound();
			}

			_context.users.Remove(user);
			await _context.SaveChangesAsync();

			return Ok(user);
		}
	}
}
