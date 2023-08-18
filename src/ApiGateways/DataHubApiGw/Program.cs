
using NLog.Extensions.Logging;
using NLog.Web;
using Ocelot.DependencyInjection;

var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);
    var app = builder.Build();
    builder.Services.AddLogging(builder =>
    {
        builder.ClearProviders();
        builder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
        builder.AddConsole();
        builder.AddNLog();
    });
    builder.Services.AddOcelot();
    app.MapGet("/", () => "Hello World!");
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

