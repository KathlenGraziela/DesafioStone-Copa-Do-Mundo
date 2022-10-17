using Microsoft.EntityFrameworkCore;
using StoneDesafio.Business.Services;
using StoneDesafio.Models;
using System.Net.Mail;

namespace StoneDesafio.Entities
{
    public class AppDbContext : DbContext
    {
        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<Clube> Clubes { get; set; }

        public DbSet<Jogo> Jogos { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) :
        base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            #region Administradores
            modelBuilder.Entity<Administrador>().HasIndex(u => u.Email).IsUnique();

            modelBuilder.Entity<Administrador>().HasData(
                new() { 
                    Id = Guid.NewGuid(),
                    Nome = "Adm1",
                    Email = "adm1@adms.com",
                    Senha = CriptografiaService.Criptografar("AdmPass")
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Nome = "Adm2",
                    Email = "adm2@adms.com",
                    Senha = CriptografiaService.Criptografar("AdmPass")
                });
            #endregion

            #region Clube
            modelBuilder.Entity<Clube>().HasIndex(u => u.Id).IsUnique();

            modelBuilder.Entity<Clube>().HasData(
                new()
                {
                    Id = 1,
                    Nome = "Clube 01",
                    Descricao = "Descrição Clube 01",
                    UrlFoto = ""
                },
                new()
                {
                    Id = 2,
                    Nome = "Clube 02",
                    Descricao = "Descrição Clube 02",
                    UrlFoto = ""
                },
                new()
                {
                    Id = 3,
                    Nome = "Clube 03",
                    Descricao = "Descrição Clube 03",
                    UrlFoto = ""
                }
            );
            #endregion

            
        }
    }
}
