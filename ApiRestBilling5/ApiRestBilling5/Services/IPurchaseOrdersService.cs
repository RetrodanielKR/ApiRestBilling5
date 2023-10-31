using ApiRestBilling5.Models;

namespace ApiRestBilling5.Services
{
    public interface IPurchaseOrdersService
    {
        Task<decimal> ChekUnitPrice(OrderItem detalle);
        Task<decimal> CalculateSubtotalOrdenItem(OrderItem item);
        decimal CalculateSubtotal(List<OrderItem> item);
    }
}
