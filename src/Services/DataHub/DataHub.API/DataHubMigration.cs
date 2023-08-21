using DataHub.API.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DataHub.API
{
    public class DataHubMigration
    {
        private readonly DataHubContext _context;

        public DataHubMigration(DataHubContext context)
        {
            _context = context;
        }
        public void Run()
        {
            if (_context.Database.CanConnect())
            {
                var pendingMigrations = _context.Database.GetPendingMigrations();
                if (pendingMigrations != null && pendingMigrations.Any())
                {
                    _context.Database.Migrate();
                }
            }
        }
    }
}
