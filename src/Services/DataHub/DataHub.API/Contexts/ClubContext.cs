using DataHub.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DataHub.API.Contexts
{
    public class ClubContext : DbContext
    {
        public ClubContext(DbContextOptions<ClubContext> options) : base(options) { }
        DbSet<ClubDto> Club { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
