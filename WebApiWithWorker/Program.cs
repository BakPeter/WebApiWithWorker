using System.Text.Json;
using WebApiWithWorker;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.Swagger;
using WebApiWithWorker.Configurations;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureAppConfiguration((context, configurationBuilder) =>
{
    // Add custom settings file
    //configurationBuilder.AddJsonFile("<file-name>");
});
// Add services to the container.
var settings = builder.Configuration.GetSection("Settings").Get<Settings>();

//LogSettings(builder.Services, settings);

//void LogSettings(IServiceCollection services, Settings settings)
//{
//    using var provider = services.BuildServiceProvider();
//    var logger = provider.GetRequiredService<ILogger<Program>>();
//    logger?.LogInformation($"Settings: {JsonSerializer.Serialize(settings)}");
//}

builder.Services.AddHostedService<Worker>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add application urls
builder.WebHost.UseUrls(settings!.ApplicationUrls.ToArray());

var app = builder.Build();

//var logger = app.Services.GetService<ILogger<Program>>();
//logger?.LogInformation($"Settings: {JsonSerializer.Serialize(settings)}");
//app.Logger.LogInformation($"Settings: {JsonSerializer.Serialize(settings)}");
//using (var serviceScope = app.Services.CreateScope())
//{
//    var logger = serviceScope.ServiceProvider.GetService<ILogger<Program>>();
//    logger?.LogWarning($"Settings: {JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true })}");
//}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// To http only comment
//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.Run("http://localhost:5559");
app.Run();
