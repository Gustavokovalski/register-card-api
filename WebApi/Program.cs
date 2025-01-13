using RegisterCard.Application;
using RegisterCard.Infrastructure;
using RegisterCard.WebApi.Extensions.Middlewares;
using RegisterCard.WebApi.Utils;
using System.Net;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ApiResponseFilter>();


builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ApiResponseFilter));
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.RegisterApplicationUseCases();
builder.Services.RegisterApplicationExternalDependencies();

builder.Services.AddHttpContextAccessor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Information);

if (!builder.Environment.IsDevelopment())
{
    var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
    builder.WebHost.ConfigureKestrel(options =>
    {
        options.Listen(IPAddress.Any, int.Parse(port)); // Listen on all network interfaces
    });
}

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();

app.UseMiddleware<ValidationHandler>();

app.MapControllers();

app.MapGet("/health", () => Results.Ok("Healthy"));

app.Run();