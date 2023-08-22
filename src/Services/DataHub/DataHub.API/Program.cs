using DataHub.API;
using DataHub.API.Contexts;
using DataHub.API.Interfaces;
using DataHub.API.Repositories;
using DataHub.API.Services;
using Microsoft.EntityFrameworkCore;
using NLog.Extensions.Logging;
using NLog.Web;

var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);
    // Add services to the container.
    builder.Services.AddControllers();
    var connString = builder.Configuration.GetConnectionString("LoggerDocker");
    var connStringLocal = builder.Configuration.GetConnectionString("LoggerLocal");
    var dataHubConnString = builder.Configuration.GetConnectionString("DataHubDocker");
    var dataHubConnStringLocal = builder.Configuration.GetConnectionString("DataHubLocal");
    builder.Services.AddDbContext<DataHubContext>(options =>
    {
        options.UseSqlServer(dataHubConnString);
    }
    );
    builder.Services.AddScoped<DataHubMigration>();
    builder.Services.AddTransient<IClubRepository, ClubRepository>();
    builder.Services.AddTransient<IClubService, ClubService>();
    builder.Services.AddHttpClient();
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddLogging(builder =>
    {
        builder.ClearProviders();
        builder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
        builder.AddConsole();
        builder.AddNLog();
    });

    var app = builder.Build();
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseAuthorization();
    app.MapControllers();
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        // Wstrzykniêcie DbContext do DataHubMigration
        var dataHubMigration = services.GetRequiredService<DataHubMigration>();

        // Wywo³anie metody Run()
        dataHubMigration.Run();
    }
    app.Run();
}
catch (Exception exception)
{
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}

