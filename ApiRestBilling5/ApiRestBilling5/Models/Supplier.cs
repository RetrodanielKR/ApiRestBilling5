using System.ComponentModel.DataAnnotations;

namespace ApiRestBilling5.Models
{
    public class Supplier
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(128)]
        public string CompanyName { get; set; } = string.Empty;
        [MaxLength(64)]
        public string ContactName { get; set; } = string.Empty;
        [MaxLength(64)]
        public string ContactTitle { get; set; } = string.Empty;
        [MaxLength(64)]
        public string City { get; set; } = string.Empty;
        [MaxLength(64)]
        public string Country { get; set; } = string.Empty;
        [MaxLength(16)]
        public string Phone { get; set; } = string.Empty;
        [EmailAddress(ErrorMessage = "Correo Invalido")]
        public string? Email { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
