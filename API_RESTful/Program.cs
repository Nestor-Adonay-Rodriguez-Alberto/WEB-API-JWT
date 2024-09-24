var builder = WebApplication.CreateBuilder(args);

// SE AGREGAN LOS CONTROLADORES DE LA API:
builder.Services.AddControllers();

// DOCUMENTACION  DE  LA  API  CON  Swagger:
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
