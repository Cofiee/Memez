using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Memez.Areas.Identity.Data;

namespace Memez.Models
{
    public class Meme
    {
        public int Id { get; set; }
        [StringLength(60, MinimumLength = 1)]
        [Required]
        public string? Title { get; set; }
        public string? ImagePath { get; set; }
        public DateTime Timestamp { get; set; }
        public MemezUser MemezUser { get; set; }
        public int VotesSum { get; set; }
    }
}
