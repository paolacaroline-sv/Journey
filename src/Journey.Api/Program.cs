using Journey.Infrastructure;
using Journey.Application.UseCases.Trips.Register;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

var databasePath = Path.GetFullPath(
    Path.Combine(builder.Environment.ContentRootPath, "..", "Journey.Infrastructure", "JourneyDatabase.db"));
var connectionString = $"Data Source={databasePath}";

builder.Services.AddDbContext<JourneyDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddScoped<RegisterTripUseCase>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => {    
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Journey API V1");
    c.RoutePrefix = string.Empty; 
});


app.MapControllers();

app.Run();
