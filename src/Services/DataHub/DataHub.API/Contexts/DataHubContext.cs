using DataHub.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataHub.API.Contexts
{
    public class DataHubContext : DbContext
    {
        public DataHubContext(DbContextOptions<DataHubContext> options) : base(options) { }
        DbSet<Club> Club { get; set; }
        DbSet<Stadium> Stadium { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Club>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<Club>(entity =>
            {
                entity.HasOne(c => c.Stadium)
                    .WithOne(s => s.Club)
                    .HasForeignKey<Club>(c => c.StadiumId);
            });
        }
    }
}
