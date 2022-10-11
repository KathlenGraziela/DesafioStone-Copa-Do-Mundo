using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Stone_Desafio.Business.Repositorys;
using Stone_Desafio.Business.Services;
using Stone_Desafio.Configuration;
using Stone_Desafio.Entities;
using Stone_Desafio.Models.Utils;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllersWithViews(o =>
{
    o.Filters.Add<HandleExceptionFilter>();
});

var connectionString = Environment.GetEnvironmentVariable("MySqlConnectionString") ?? 
                       builder.Configuration.GetConnectionString("MySqlConnectionString");

builder.Services.AddDbContext<AppDbContext>(options => options.UseMySQL(connectionString));



builder.Services.AddScoped<AdministradorRepository>();
builder.Services.AddScoped<AdministradorService>();
builder.Services.AddSingleton<ModelConverter>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var dbContext = services.GetRequiredService<AppDbContext>();
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
