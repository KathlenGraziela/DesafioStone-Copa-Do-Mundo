using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StoneDesafio.Business.Repositorys;
using StoneDesafio.Business.Services;
using StoneDesafio.Data.ClubeDtos;
using StoneDesafio.Data.GrupoDtos;
using StoneDesafio.Data.JogoDtos;
using StoneDesafio.Data.ResultadoDtos;
using StoneDesafio.Entities;
using StoneDesafio.Models;
using StoneDesafio.Models.Utils;
using StoneDesafio.Services;
using System.Text;

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
builder.Services.AddGenericRepository<IRepository<FaseCampeonato>, AppDbContext>();

builder.Services.AddScoped<IService<Clube, ClubeCriarDto, ClubeEditarDto>, ClubeService>();
builder.Services.AddScoped<IService<Grupo, GrupoCriarDto, GrupoEditarDto>, GrupoService>();
builder.Services.AddScoped<IService<Jogo, JogoCriarDto, JogoEditarDto>, JogoService>();

builder.Services.AddScoped<AdministradorService>();
builder.Services.AddScoped<LoginService>();

builder.Services.AddSingleton<ModelConverter>();

//builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
//{
//    builder.AllowAnyOrigin()
//           .AllowAnyMethod()
//           .AllowAnyHeader();
//}));

//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
    });

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
        //dbContext.Database.EnsureCreated();

    }

    app.UseHsts();
}
app.UseStaticFiles();
app.UseRouting();

//app.UseCors("*");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

//app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

public partial class Program { }