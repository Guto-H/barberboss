using BarberBoss.API.Filter;
using BarberBoss.API.Middleware;
using BarberBoss.Infrastructure;
using BarberBoss.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Monstrando o filtro de excessões para a API
builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

// Usando injeção de dependencia
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();


var app = builder.Build();

// Usando o Middleware que foi criado
app.UseMiddleware<CultureMiddleware>();

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
