using WebApiWithWorker;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApiWithWorker.Configurations;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureAppConfiguration((context, configurationBuilder) =>
{
    // Add custom settings file
    //configurationBuilder.AddJsonFile("<file-name>");
});
// Add services to the container.
var settings = builder.Configuration.GetSection("Settings").Get<Settings>();

builder.Services.AddHostedService<Worker>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add application urls
builder.WebHost.UseUrls(settings!.ApplicationUrls.ToArray());

var app = builder.Build();

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
