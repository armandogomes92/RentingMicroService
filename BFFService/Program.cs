using BFFService.Services;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRefitClient<IMotorcycleService>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:5002"));
builder.Services.AddRefitClient<IDeliveryManService>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:5003"));
builder.Services.AddRefitClient<IRentalService>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:5001"));

builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.DocInclusionPredicate((_, api) => true);
    c.OrderActionsBy(api => api.GroupName);
});

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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sistema de Manuten��o de Motos V1");
        c.DocumentTitle = "Sistema de Manuten��o de Motos";
    });
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();