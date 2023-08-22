using Microsoft.EntityFrameworkCore;
using NLog.Extensions.Logging;
using NLog.Web;
using WebScrapingIntegration.API.Contexts;
using WebScrapingIntegration.API.Interfaces;
using WebScrapingIntegration.API.Repositories;
using WebScrapingIntegration.API.Services;

var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddControllers();
    var loggerConnString = builder.Configuration.GetConnectionString("LoggerDocker");
    var loggerConnStringLocal = builder.Configuration.GetConnectionString("LoggerLocal");
    var webScrapConnString = builder.Configuration.GetConnectionString("WebScrapingIntegrationDocker");
    var webScrapConnStringLocal = builder.Configuration.GetConnectionString("WebScrapingIntegrationLocal");
    builder.Services.AddDbContext<WebScrapingProcessesContext>(options =>
        options.UseSqlServer(webScrapConnString));
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddLogging(builder =>
    {
        builder.ClearProviders();
        builder.SetMinimumLevel(LogLevel.Trace);
        builder.AddConsole();
        builder.AddNLog();
    });
    builder.Services.AddTransient<IClubScrapingService, ClubScrapingService>();
    builder.Services.AddTransient<IWebScrapingProcessesRepository, WebScrapingProcessesRepository>();
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseAuthorization();
    app.MapControllers();

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

