using Microsoft.EntityFrameworkCore;
using StoneDesafio.Business.Services;
using StoneDesafio.Enum;
using StoneDesafio.Models;
using System.Net.Mail;
using System.Reflection.Emit;

namespace StoneDesafio.Entities
{
    public class AppDbContext : DbContext
    {
        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<Clube> Clubes { get; set; }
        public DbSet<Jogo> Jogos { get; set; }
        public DbSet<Grupo> Grupos { get; set; }
        public DbSet<Resultado> Resultados { get; set; } 
        public DbSet<FaseCampeonato> FaseCampeonatos { get; set; }


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

        public void SeedDb()
        {

            var jogos = new List<Jogo>()
            {
                new ()
                {
                    Nome = "Barcelona vs Real Madri",
                    Id = 1,
                    ClubeA = new()
                    {
                        Id = 1,
                        Descricao = "n/a",
                        Nome = "Barcelona",
                        UrlFoto = "https://pbs.twimg.com/profile_images/1542795679956930561/ZgR6xp8c_400x400.jpg"
                    },
                    ClubeB = new()
                    {
                        Id = 2,
                        Descricao = "n/a",
                        Nome = "Real Madrid",
                        UrlFoto = "https://www.realmadrid.com/StaticFiles/RealMadridResponsive/images/static/og-image.png"
                    },
                    GolsA = 3,
                    GolsB = 2
                }
            };

            var jogos2 = new List<Jogo>()
            {
                new ()
                {
                    Nome = "Marocos vs Trade",
                    Id = 2,
                    ClubeA = new()
                    {
                        Id = 3,
                        Descricao = "n/a",
                        Nome = "Marocos",
                        UrlFoto = "https://upload.wikimedia.org/wikipedia/pt/7/71/F%C3%A9d%C3%A9ration_Royale_Marocaine_de_Football.png"
                    },
                    ClubeB = new()
                    {
                        Id = 4,
                        Descricao = "n/a",
                        Nome = "Brasil",
                        UrlFoto = "https://cf.shopee.com.br/file/cbeb085f3ccc2acf1e5eaf8fcaa65eb0"
                    },
                    GolsA = 3,
                    GolsB = 2
                }
            };

            FaseCampeonatos.Add(
            new()
            {
                Id = 1,
                FaseAtualCampeonato = FasesCampeonato.Grupo,
                Jogos = jogos
            });
            FaseCampeonatos.Add(
            new()
            {
                Id = 2,
                FaseAtualCampeonato = FasesCampeonato.Oitavas,
                Jogos = jogos2
            });

            SaveChanges();
        }
    }
}
