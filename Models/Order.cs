using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBanNuoc.Models
{
    public enum OrderStatus
    {
        Pending,
        Processing,
        Shipping,
        Completed,
        Cancelled
    }

    public class Order
    {
        public int Id { get; set; }
        
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        
        public DateTime OrderDate { get; set; } = DateTime.Now;
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }
        
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        
        [Required]
        public string ShippingAddress { get; set; }
        
        public string? PhoneNumber { get; set; }
        
        public string PaymentMethod { get; set; } // COD, Card, etc.
        
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
