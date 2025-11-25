using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBanNuoc.Models
{
    public class ProductOption
    {
        public int Id { get; set; }
        
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Group { get; set; } // e.g., "Size", "Sugar", "Ice", "Topping"
        
        [Required]
        [StringLength(50)]
        public string Name { get; set; } // e.g., "Large", "50%", "Pearl"
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal PriceAdjustment { get; set; } = 0;
    }
}
