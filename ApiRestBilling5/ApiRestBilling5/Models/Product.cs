﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiRestBilling5.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(128)]
        public string ProductName { get; set; } = string.Empty;
        [Required]
        public int SupplierId { get; set; }
        public decimal UnitPrice { get; set; } = 0;
        [MaxLength(64)]
        public string Package { get; set; } = string.Empty;
        public bool IsDiscontinued { get; set; } = false;
        [ForeignKey("SupplierId")]
        public Supplier Supplier { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }

    }
}
