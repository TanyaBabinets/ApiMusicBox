using ApiMusicBox.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using System;
using System.Data;
using Microsoft.AspNetCore.Hosting;
namespace ApiMusicBox.Controllers
{
    [ApiController]
    [Route("api/songs")]
    public class SongController :ControllerBase
    {
        private readonly SongContext _context;
        private readonly IWebHostEnvironment _appEnvironment;

		public SongController(SongContext context, IWebHostEnvironment _appEnvironment)
        {
            this._context = context;
			this._appEnvironment = _appEnvironment;
        }

        [HttpGet]
        [RequestSizeLimit(2147483648)]
        public async Task<ActionResult<IEnumerable<Songs>>> GetSongs()
        {                 

            var list= await _context.songs.Include(s => s.user).Include(s => s.genre).ToListAsync();
			foreach (var a in list)
			{
				a.dateView = a.Datetime.Value.ToString("dd-MM-yyyy");
			}
			return list;
        }
		// GET: api/Songs/3
		[HttpGet("{id}")]
        [RequestSizeLimit(2147483648)]
        public async Task<ActionResult<Songs>> GetSong(int id)
		{
			var song = await _context.songs.Include(s => s.user).Include(s => s.genre).SingleOrDefaultAsync(m => m.Id == id);
			
			if (song == null)
			{
				return NotFound();
			}
			song.dateView = song.Datetime.Value.ToString("yyyy-MM-dd");
			return new ObjectResult(song);
		}

		// PUT: api/Songs
		[HttpPut]
        [RequestSizeLimit(2147483648)]
        public async Task<ActionResult<Songs>> PutSong(SongModel song)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			if (!_context.songs.Any(e => e.Id == song.Id))
			{
				return NotFound();
			}
			var updsong = await _context.songs.FindAsync(song.Id);

            updsong.name = song.name;
			updsong.singer = song.singer;
			updsong.runtime = song.runtime;
			updsong.size = song.size;
			updsong.Datetime = DateTime.Now;
			//updsong.dateView = song.Datetime;

			if (song.picfile != null)
			{
				string path = "/img/" + song.picfile.FileName;
				using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
				{
					await song.picfile.CopyToAsync(fileStream);
				}
				updsong.pic = path;
			}
			if (song.musfile != null)
			{
				string path1 = "/mp3/" + song.musfile.FileName;
				using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path1, FileMode.Create))
				{
					await song.musfile.CopyToAsync(fileStream);
				}
				updsong.file = path1;
			}
			updsong.genre = await _context.genres.FindAsync(song.genre);
			updsong.user = await _context.users.Where(e => e.Login == song.user).FirstOrDefaultAsync();
			Console.WriteLine(updsong.name);
			_context.Update(updsong);
			await _context.SaveChangesAsync();
			return Ok(updsong);
		}

		// POST: api/Songs
		[HttpPost]
        [RequestSizeLimit(2147483648)]
        public async Task<ActionResult<Songs>> PostSong(SongModel s)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var newsong = new Songs();
			
			newsong.name = s.name;
			newsong.singer = s.singer;
			newsong.runtime = s.runtime;
			newsong.size = s.size;
			newsong.Datetime = DateTime.Now;
			
			if (s.picfile != null)
			{
				string path = "/img/" + s.picfile.FileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await s.picfile.CopyToAsync(fileStream);
                }

                newsong.pic = path;
			}
			if (s.musfile != null)
			{
				string path1 = "/mp3/" + s.musfile.FileName;
				using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path1, FileMode.Create))
				{
					await s.musfile.CopyToAsync(fileStream);
				}
				newsong.file = path1;
			}
			newsong.genre = await _context.genres.FindAsync(s.genre);
			newsong.user = await _context.users.Where(e => e.Login == s.user).FirstOrDefaultAsync();



			_context.songs.Add(newsong);
			await _context.SaveChangesAsync();

			return Ok(s);
		}

		// DELETE: api/Songs/3
		[HttpDelete("{id}")]
		public async Task<ActionResult<Songs>> DeleteSong(int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var song = await _context.songs.SingleOrDefaultAsync(m => m.Id == id);
			if (song == null)
			{
				return NotFound();
			}

			_context.songs.Remove(song);
			await _context.SaveChangesAsync();

			return Ok(song);
		}
	}
}

       