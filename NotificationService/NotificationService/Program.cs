using Microsoft.Extensions.Options;
using NotificationService.Infrastructure.Messaging;
using NotificationService.Infrastructure.MongoDB;
using NotificationService.Service.Consumers;
using NotificationService.Service.Services;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<RabbitMQConfiguration>(builder.Configuration.GetSection("RabbitMQ"));
builder.Services.AddSingleton(sp =>
            sp.GetRequiredService<IOptions<RabbitMQConfiguration>>().Value);
builder.Services.AddSingleton<RabbitMQClient>();

// Adiciona o serviço IConnection e IModel
builder.Services.AddSingleton<IConnection>(sp =>
{
    var factory = new ConnectionFactory
    {
        HostName = sp.GetRequiredService<IOptions<RabbitMQConfiguration>>().Value.Host
    };
    return factory.CreateConnection();
});

builder.Services.AddSingleton<IModel>(sp =>
{
    var connection = sp.GetRequiredService<IConnection>();
    return connection.CreateModel();
});

builder.Services.AddHostedService<ConsumerTotalPrice>();
builder.Services.AddHostedService<ConsumerMotorcycle2024>();
builder.Services.AddHostedService<ConsumerRegisteredMotorcycle>();

builder.Services.AddSingleton(builder.Configuration.GetSection("MongoDBConfiguration").Get<MongoConfig>()!);
builder.Services.AddSingleton<MongoService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowedHosts",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowedHosts");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();