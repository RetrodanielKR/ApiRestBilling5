using Microsoft.EntityFrameworkCore;

namespace ApiRestBilling5.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base (options)
        {
            
        }
    }
}
