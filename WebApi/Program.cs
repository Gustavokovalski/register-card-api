using Microsoft.EntityFrameworkCore;
using RegisterCard.Application;
using RegisterCard.Infrastructure;
using RegisterCard.Infrastructure.Context;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Configure Kestrel to listen on a specific port from the environment variable
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(IPAddress.Any, int.Parse(port)); // Listen on all network interfaces
});

//TODO - jogar para infra
builder.Services.AddDbContext<CardDbContext>(options =>
    options.UseInMemoryDatabase("CardDatabase"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.RegisterApplicationUseCases();
builder.Services.RegisterApplicationExternalDependencies();

builder.Services.AddHttpContextAccessor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Debug);

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

//app.UseMiddleware<ValidationMiddleware>();

app.MapControllers();

app.MapGet("/health", () => Results.Ok("Healthy"));

app.Run();