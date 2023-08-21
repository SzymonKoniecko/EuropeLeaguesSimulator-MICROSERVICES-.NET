using NLog.Extensions.Logging;
using NLog.Web;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddLogging(builder =>
    {
        builder.ClearProviders();
        builder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
        builder.AddConsole();
        builder.AddNLog();
    });
    builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.Local.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();
    builder.Services.AddOcelot(builder.Configuration);
    var app = builder.Build();


    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    await app.UseOcelot();
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

