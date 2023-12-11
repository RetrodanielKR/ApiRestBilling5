using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiRestBilling5.Data;
using ApiRestBilling5.Models;
using ApiRestBilling5.Services;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestBilling5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IPurchaseOrdersService _purchaseOrdersService;

        public OrderController(ApplicationDbContext context, IPurchaseOrdersService purchaseOrdersService)
        {
            _context = context;
            _purchaseOrdersService = purchaseOrdersService;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.Include(oi => oi.OrderItems).ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders.Include(oi => oi.OrderItems)
                                     .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Orders
        [HttpPost]
        // POST: api/Orders
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            // Verifica si el cliente existe
            var customer = await _context.Customers.FindAsync(order.CustomerId);
            if (customer == null)
            {
                return BadRequest("El cliente no existe.");
            }

            // Verifica si los productos existen y calcula los subtotales
            foreach (var item in order.OrderItems)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product == null)
                {
                    return BadRequest($"El producto con ID {item.ProductId} no existe.");
                }

                item.UnitPrice = product.UnitPrice;
                item.Subtotal = item.UnitPrice * item.Quantity;
            }

            // Calcula el total de la orden
            order.TotalAmount = order.OrderItems.Sum(item => item.Subtotal);

            // Agrega la orden a la base de datos
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }


        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}

