using ApiRestBilling5.Data;
using ApiRestBilling5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiRestBilling5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SuppliersController(ApplicationDbContext context)
        {
            this._context = context;
        }

        // GET: api/<SuppliersController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Supplier>>> Get()
        {
            var suppliers = await _context.Suppliers.ToListAsync();
            if (suppliers == null) 
            {
                return NotFound();
            }
            return suppliers;
        }

        // GET api/<SuppliersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Supplier>> Get(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);

            if (supplier == null)
            {
                return NotFound();
            }

            return supplier;
        }

        // POST api/<SuppliersController>

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Supplier>>> Post([FromBody] List<Supplier> supplier)
        {
            if (_context.Suppliers == null || supplier == null || supplier.Count == 0)
            {
                return BadRequest("Datos no válidos o faltantes.");
            }

            _context.Suppliers.AddRange(supplier);
            await _context.SaveChangesAsync();

            return supplier;
        }

        // PUT api/<SuppliersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Supplier updatedSupplier)
        {
            if (id != updatedSupplier.Id)
            {
                return BadRequest("Mal echo");
            }

            _context.Entry(updatedSupplier).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupplierExists(id))
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

        // DELETE api/<SuppliersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);

            if (supplier == null)
            {
                return NotFound();
            }

            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SupplierExists(int id)
        {
            return _context.Suppliers.Any(s => s.Id == id);
        }
    }
}
