using API_RESTful.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// SE AGREGAN LOS CONTROLADORES DE LA API:
builder.Services.AddControllers();

// DOCUMENTACION  DE  LA  API  CON  Swagger:
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// INYECCION DE LA DB:
builder.Services.AddDbContext<MyDBcontext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Cadena_Conexion")));


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
