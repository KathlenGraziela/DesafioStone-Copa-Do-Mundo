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
        public DbSet<Grupo> Grupos { get; set; }
        public DbSet<Resultado> Resultados { get; set; }

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
                    Id = 1,
                    Nome = "Adm1",
                    Email = "adm1@adms.com",
                    Senha = CriptografiaService.Criptografar("AdmPass")
                },
                new()
                {
                    Id = 2,
                    Nome = "Adm2",
                    Email = "adm2@adms.com",
                    Senha = CriptografiaService.Criptografar("AdmPass")
                });
            #endregion

            #region Clube
            modelBuilder.Entity<Clube>().HasIndex(u => u.Id).IsUnique();
            #endregion

            #region Jogo
            modelBuilder.Entity<Jogo>().HasIndex(u => u.Id).IsUnique();
            #endregion

            #region Resultado
            modelBuilder.Entity<Resultado>().HasIndex(u => u.Id).IsUnique();
            #endregion

            #region Grupo
            modelBuilder.Entity<Grupo>().HasIndex(u => u.Id).IsUnique();
            #endregion

        }
    }
}
