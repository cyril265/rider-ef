using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FooDbContext>(optionsBuilder =>
{
    optionsBuilder.UseNpgsql("npgsqlDataSource", o =>
        {
            o.MigrationsAssembly(typeof(FooDbContext).Assembly.FullName);
            o.MigrationsHistoryTable("__EFMigrationsHistory", "public");
            o.UseNodaTime();
        })
        .UseSnakeCaseNamingConvention()
        .UseProjectables();
});


var app = builder.Build();

var dbContext = app.Services.GetRequiredService<FooDbContext>();

var persons = dbContext.Person.First();
var personsAddresses = persons.Address;
Console.WriteLine(personsAddresses);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}