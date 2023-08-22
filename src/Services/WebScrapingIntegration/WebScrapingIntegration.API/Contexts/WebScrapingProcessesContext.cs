using Microsoft.EntityFrameworkCore;
using WebScrapingIntegration.API.Entities;

namespace WebScrapingIntegration.API.Contexts
{
    public class WebScrapingProcessesContext : DbContext
    {
        public WebScrapingProcessesContext(DbContextOptions<WebScrapingProcessesContext> options) : base(options)
        {

        }
        public DbSet<WebScrapingProces> WebScrapingProces { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<WebScrapingProces>()
                .HasKey(wsp => wsp.Id);
        }
    }
}
