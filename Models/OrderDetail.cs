using System.ComponentModel.DataAnnotations.Schema;

namespace WebBanNuoc.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        
        public int Quantity { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; } // Price at time of purchase
        
        public string? SelectedOptions { get; set; } // e.g., "Size: L, Sugar: 50%"
    }
}
