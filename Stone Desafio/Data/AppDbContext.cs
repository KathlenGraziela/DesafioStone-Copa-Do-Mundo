using Microsoft.EntityFrameworkCore;
using Stone_Desafio.Models;
using System.Net.Mail;

namespace Stone_Desafio.Entities
{
    public class AppDbContext : DbContext
    {
        public DbSet<Administrador> Administradores { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) :
        base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Administrador>().HasIndex(u => u.Email).IsUnique();

            modelBuilder.Entity<Administrador>().HasData(
                new() { 
                    Id = Guid.NewGuid(),
                    Nome = "Adm1",
                    Email = "adm1@adms.com",
                    Senha = "AdmPass"
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Nome = "Adm2",
                    Email = "adm2@adms.com",
                    Senha = "AdmPass"
                });
        }
    }
}
