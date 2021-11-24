using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks()
    .AddCheck("live", () => File.Exists(@"/tmp/live-error.txt") ? HealthCheckResult.Unhealthy() : HealthCheckResult.Healthy())
    .AddCheck("file", () => File.Exists(@"/tmp/ready-error.txt") ? HealthCheckResult.Unhealthy() : HealthCheckResult.Healthy(), tags: new[] {"services"});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHealthChecks("/health/live", new HealthCheckOptions
{
    Predicate = r => r.Name.Contains("live")
});

app.UseHealthChecks("/health/ready", new HealthCheckOptions
 {
    Predicate = r => r.Tags.Contains("services")
 });

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
