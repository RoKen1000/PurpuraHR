using Microsoft.EntityFrameworkCore;

namespace Purpura.DataAccess.DataContext
{
    public class PurpuraDbContext : DbContext
    {
        public PurpuraDbContext(DbContextOptions<PurpuraDbContext> options) : base(options)
        {
            
        }


    }
}
