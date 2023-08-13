using Logger.API.Configuration;
using Logger.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Logger.API.Context
{
    public class LoggerContext : DbContext
    {
        public LoggerContext(DbContextOptions<LoggerContext> options) : base(options)
        {

        }
        public DbSet<AppLogDto> AppLogs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AppLogConfiguration());
        }
    }
}
