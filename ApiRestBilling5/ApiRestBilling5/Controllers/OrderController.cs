using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiRestBilling5.Data;
using ApiRestBilling5.Models;
using Microsoft.IdentityModel.Protocols.OpenIdConnect.Configuration;
using System.Security.Cryptography;
using System.Reflection.Metadata.Ecma335;
using ApiRestBilling5.Services;

namespace ApiRestBilling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private readonly IPurchaseOrdersService _purchaseOrdersService;
        private OrderItem detalle;

        public OrderController(ApplicationDbContext context, IPurchaseOrdersService purchaseOrdersService)
        {
            _context = context;
            _purchaseOrdersService = purchaseOrdersService; 
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            return await _context.Orders.Include(oi => oi.OrderItems).ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            // var order = await _context.Orders.Include(oi => oi.OrderItems).FindAsync(id);
            var order = await _context.Orders.Include(oi => oi.OrderItems)
                                     .FirstOrDefaultAsync(o => o.Id == id); // Asumiendo que el nombre del campo es 'Id'.

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            if (_context.Orders == null)

            {
                return Problem("Entity set 'ApplicationDbContext.Orders'  is null.");
            }


            foreach (var datelle in order.OrderItems)

            {
                datelle.UnitPrice = await _purchaseOrdersService.CheckUnitPrice(detalle);

                datelle.Subtotal = await _purchaseOrdersService.CalculateSubtotalOrderItem(detalle);

            }

            order.TotalAmount = _purchaseOrdersService.CalcularTotalOrderItems((List<OrderItem>)order.OrderItems);
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getorder", new { id = order.Id }, order);

        }
           
        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
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
            return (_context.Orders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
