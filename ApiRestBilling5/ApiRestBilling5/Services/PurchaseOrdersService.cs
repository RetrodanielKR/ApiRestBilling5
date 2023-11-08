using ApiRestBilling5.Data;
using ApiRestBilling5.Models;

namespace ApiRestBilling5.Services
{
    public class PurchaseOrdersService : IPurchaseOrdersService
    {
        private readonly ApplicationDbContext _context;
        public PurchaseOrdersService(ApplicationDbContext context) 
        {
            _context = context;
        }
        public async Task<Product> GetProductById(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) 
            {
                throw new Exception($"No esta el {productId} producto.");
            }
            return product;
        }
        public async Task<decimal> CheckUnitPrice(OrderItem detalle)
        {
            var producto = await _context.Products.FindAsync(detalle.ProductId);
            detalle.UnitPrice = producto?.UnitPrice ?? 0;
            return (decimal)detalle.UnitPrice;
        }
        public async Task<decimal> CalculateSubtotalOrderItem(OrderItem item)
        {
            decimal unitPrice = await CheckUnitPrice(item);
            item.Subtotal = unitPrice * item.Quantity;
            return (decimal)item.Subtotal;
        }
        public decimal CalcularTotalOrderItems(List<OrderItem> items) 
        {
            decimal total = 0;
            foreach (var item in items) 
            {
                total += (decimal)item.Subtotal;
            }
            return total;
        }
    }
}
