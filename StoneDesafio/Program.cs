using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StoneDesafio.Business.Repositorys;
using StoneDesafio.Business.Services;
using StoneDesafio.Configuration;
using StoneDesafio.Entities;
using StoneDesafio.Models.Utils;
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

//builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
//{
//    builder.AllowAnyOrigin()
//           .AllowAnyMethod()
//           .AllowAnyHeader();
//}));

//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddControllersWithViews();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler();

    using (var scope = app.Services.CreateScope())
    {
        
        var services = scope.ServiceProvider;
        var dbContext = services.GetRequiredService<AppDbContext>();
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
    }

    app.UseHsts();
}
app.UseStaticFiles();

app.UseRouting();

//app.UseCors("*");

app.UseHttpsRedirection();

app.UseAuthorization();

//app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
