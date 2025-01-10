using RegisterCard.Application;
using RegisterCard.Infrastructure;
using RegisterCard.Infrastructure.Context;
using RegisterCard.WebApi.Extensions.Middlewares;

var builder = WebApplication.CreateBuilder(args);

//TODO - jogar para infra
builder.Services.AddDbContext<CardDbContext>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.RegisterApplicationUseCases();
builder.Services.RegisterApplicationExternalDependencies();

builder.Services.AddHttpContextAccessor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthorization();

app.UseMiddleware<ValidationMiddleware>();

app.MapControllers();

app.Run();