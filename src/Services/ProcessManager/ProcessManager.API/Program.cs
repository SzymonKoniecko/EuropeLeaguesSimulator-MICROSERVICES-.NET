using DataHub.API;
using Microsoft.Extensions.Options;
using NLog.Extensions.Logging;
using NLog.Web;
using RabbitMQ.Client;

var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddControllers();
    var connString = builder.Configuration.GetConnectionString("LoggerDocker");
    var connStringLocal = builder.Configuration.GetConnectionString("LoggerLocal");
    /*builder.Services.AddDbContext<LoggerContext>(options =>
        options.UseSqlServer(connString));*/
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddLogging(builder =>
    {
        builder.ClearProviders();
        builder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
        builder.AddConsole();
        builder.AddNLog();
    });
    builder.Services.Configure<RabbitMQOptions>(builder.Configuration.GetSection("RabbitMQ"));
    builder.Services.AddSingleton<IConnection>(sp =>
    {
        var options = sp.GetRequiredService<IOptions<RabbitMQOptions>>().Value;
        var factory = new ConnectionFactory
        {
            HostName = options.HostName,
            Port = options.Port, // Set the port
            UserName = options.UserName,
            Password = options.Password,
        };
        return factory.CreateConnection();
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

