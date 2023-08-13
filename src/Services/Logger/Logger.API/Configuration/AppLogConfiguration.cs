using Logger.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logger.API.Configuration
{
    public class AppLogConfiguration : IEntityTypeConfiguration<AppLogDto>
    {
        public void Configure(EntityTypeBuilder<AppLogDto> builder)
        {
            builder.ToTable("AppLogs");
            builder.HasKey(x => x.Id);
            builder.Property(e => e.Id).HasColumnName("Id");
            builder.Property(e => e.Level).IsRequired();
            builder.Property(e => e.Message).IsRequired();
            builder.Property(e => e.Exception).IsRequired(false);
            builder.Property(e => e.LoggedTime).IsRequired();
            builder.Property(e => e.Callsite).IsRequired();
        }
    }
}
