using Microsoft.EntityFrameworkCore;
using StoneDesafio.Business.Repositorys;
using StoneDesafio.Business.Services;
using StoneDesafio.Data.ClubeDtos;
using StoneDesafio.Data.JogoDtos;
using StoneDesafio.Data.ResultadoDtos;
using StoneDesafio.Entities;
using StoneDesafio.Models;
using StoneDesafio.Models.Utils;
using StoneDesafio.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var connectionString = Environment.GetEnvironmentVariable("MySqlConnectionString") ?? 
                       builder.Configuration.GetConnectionString("MySqlConnectionString");

builder.Services.AddDbContext<AppDbContext>(options => options.UseMySQL(connectionString));

builder.Services.AddGenericRepository<IRepository<Administrador>, AppDbContext>();
builder.Services.AddGenericRepository<IRepository<Jogo>, AppDbContext>();
builder.Services.AddGenericRepository<IRepository<Clube>, AppDbContext>();
builder.Services.AddGenericRepository<IRepository<Grupo>, AppDbContext>();
builder.Services.AddGenericRepository<IRepository<Resultado>, AppDbContext>();

builder.Services.AddScoped<AdministradorService>();
builder.Services.AddScoped<IService<Clube, ClubeCriarDto, ClubeEditarDto>, ClubeService>();
builder.Services.AddScoped<IService<Resultado, ResultadoCriarDto, ResultadoEditarDto>, ResultadoService>();
builder.Services.AddScoped<IService<Jogo, JogoCriarDto, JogoEditarDto>, JogoService>();
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
        //dbContext.Database.EnsureDeleted();
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