using NotificationService.Infrastructure.Messaging;
using NotificationService.Infrastructure.MongoDB;
using NotificationService.Service.Consumers;
using NotificationService.Service.Services;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(builder.Configuration.GetSection("RabbitMQConfiguration").Get<RabbitMQConfiguration>()!);
builder.Services.AddSingleton<RabbitMQClient>();
builder.Services.AddSingleton<IModel>(sp => sp.GetRequiredService<RabbitMQClient>().CreateModel());

builder.Services.AddHostedService<ConsumerTotalPrice>();

builder.Services.AddSingleton(builder.Configuration.GetSection("MongoDBConfiguration").Get<MongoConfig>()!);
builder.Services.AddSingleton<MongoService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
