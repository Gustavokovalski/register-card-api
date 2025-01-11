using Microsoft.EntityFrameworkCore;
using RegisterCard.Application;
using RegisterCard.Infrastructure;
using RegisterCard.Infrastructure.Context;

var builder = WebApplication.CreateBuilder(args);

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


var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Add($"http://*:{port}");

app.UseHttpsRedirection();

app.UseAuthorization();

//app.UseMiddleware<ValidationMiddleware>();

app.MapControllers();

app.MapGet("/health", () => Results.Ok("Healthy"));

app.Run();