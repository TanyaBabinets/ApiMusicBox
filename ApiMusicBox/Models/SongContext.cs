using Microsoft.EntityFrameworkCore;

namespace ApiMusicBox.Models
{
    public class SongContext : DbContext
    {
        public DbSet<Users> users { get; set; }
        public DbSet<Songs> songs { get; set; }
        public DbSet<Genres> genres { get; set; }

        public SongContext(DbContextOptions<SongContext> options)
           : base(options)

        {
            if (Database.EnsureCreated())
            {
            }
        }
    }

}
