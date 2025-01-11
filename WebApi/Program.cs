using RegisterCard.Application;
using RegisterCard.Infrastructure;
using RegisterCard.WebApi.Extensions.Middlewares;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

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

if (!builder.Environment.IsDevelopment())
{
    var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
    builder.WebHost.ConfigureKestrel(options =>
    {
        options.Listen(IPAddress.Any, int.Parse(port)); // Listen on all network interfaces
    });
}

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();

app.UseMiddleware<ValidationMiddleware>();

app.MapControllers();

app.MapGet("/health", () => Results.Ok("Healthy"));

app.Run();