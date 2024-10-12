using NotificationService.Infrastructure.Messaging;
using NotificationService.Infrastructure.MongoDB;
using NotificationService.Service.Consumers;
using NotificationService.Service.Services;
using RabbitMQ.Client;
using static MongoDB.Driver.WriteConcern;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(builder.Configuration.GetSection("RabbitMQConfiguration").Get<RabbitMQConfiguration>());
builder.Services.AddSingleton<RabbitMQClient>();
builder.Services.AddSingleton<IModel>(sp => sp.GetRequiredService<RabbitMQClient>().CreateModel());
builder.Services.AddHostedService<ConsumerTotalPrice>();

builder.Services.AddSingleton(builder.Configuration.GetSection("MongoDBConfiguration").Get<MongoConfig>());
builder.Services.AddSingleton<MongoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
