using Memez.Areas.Identity.Data; 

namespace Memez.Models
{
    public class Vote
    {
        public int Id { get; set; }
        public Meme Meme { get; set; }
        public MemezUser MemezUser { get; set; }
    }
}
