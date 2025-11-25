using System.ComponentModel.DataAnnotations;

namespace WebBanNuoc.Models
{
    public class Review
    {
        public int Id { get; set; }
        
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        
        [Range(1, 5)]
        public int Rating { get; set; }
        
        public string? Comment { get; set; }
        
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
