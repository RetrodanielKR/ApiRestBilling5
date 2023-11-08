using ApiRestBilling5.Models;

namespace ApiRestBilling5.Services
{
    public interface IPurchaseOrdersService
    {
        Task<decimal> CheckUnitPrice(OrderItem detalle);
        Task<decimal> CalculateSubtotalOrderItem(OrderItem item);
        decimal CalcularTotalOrderItems(List<OrderItem> items);
    }
}
