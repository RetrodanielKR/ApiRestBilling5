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
                throw new Exception($"El producto con ID {productId} no fue encontrado.");
            }
            return product;
        }

        public async Task<decimal> ChekUnitPrice(OrderItem detalle)
        {
            var producto = await GetProductById(detalle.ProductId);
            detalle.UnitPrice = producto.UnitPrice;
            return (decimal)producto.UnitPrice;      
        }

        public async Task<decimal> CalculateSubtotalOrdenItem(OrderItem item)
        {
            decimal unitPrice = await ChekUnitPrice(item);
            item.Subtotal = unitPrice * item.Quantity;

            return (decimal)item.Subtotal;
                                                         
        }

        public decimal CalculateTotalOrderIteams(List<OrderItem> items)
        {
            decimal total = 0;
            foreach (var item in items) 
            {
                total += (decimal)item.Subtotal;
            }
            return total;
        }

        public decimal CalculateSubtotal(List<OrderItem> item)
        {
            throw new NotImplementedException();
        }
    }
}
