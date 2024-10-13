var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.DocInclusionPredicate((_, api) => true);
    c.OrderActionsBy(api => api.GroupName);
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sistema de Manutenção de Motos V1");
    c.DocumentTitle = "Sistema de Manutenção de Motos";
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
