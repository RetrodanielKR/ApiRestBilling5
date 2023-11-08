using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiRestBilling5.Models
{
    public class Order
    {

    
            [Key]
            public int Id { get; set; }
            [Required]
            public DateTime OrderDate { get; set; } = DateTime.Now;
            public Guid OrderNumber { get; set; } = Guid.NewGuid();
            [Required]
            public int CustomerId { get; set; }
            public decimal TotalAmount { get; set; } = 0;
            [ForeignKey("CustomerId")]
            public Customer? Customer { get; set; }
            public virtual ICollection<OrderItem> OrderItems { get; set; }

        }
}

