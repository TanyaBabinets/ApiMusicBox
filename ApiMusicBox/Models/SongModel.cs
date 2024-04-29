using System.ComponentModel.DataAnnotations;

namespace ApiMusicBox.Models
{
	public class SongModel
	{
		public int Id { get; set; }
		public string? name { get; set; }
		
		public string? singer { get; set; }
	
		public string? runtime { get; set; }
	
		public double? size { get; set; }
		public DateTime? DateTime { get; set; }
		public int genre { get; set; }
		public string? user { get; set; }	
		public IFormFile? musfile { get; set; }
	    public IFormFile? picfile { get; set; }
		
	}
}
