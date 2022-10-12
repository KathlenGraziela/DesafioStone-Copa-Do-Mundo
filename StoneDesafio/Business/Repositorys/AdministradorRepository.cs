using Microsoft.EntityFrameworkCore;
using StoneDesafio.Businesss;
using StoneDesafio.Entities;
using StoneDesafio.Models;

namespace StoneDesafio.Business.Repositorys
{
    public class AdministradorRepository
    {
        private readonly AppDbContext dbContext;

        public AdministradorRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Administrador>> SelectTopNAsync(int n = 10) => 
            await dbContext.Administradores.Take(n).ToListAsync();

        public async Task<Administrador?> SelectByIdAsync(Guid id) =>
            await dbContext.Administradores.FirstOrDefaultAsync(a => a.Id == id);

        public async Task<Administrador> SelectByIdRequiredAsync(Guid id) =>
            await dbContext.Administradores.FirstOrDefaultAsync(a => a.Id == id) ??
            throw new ApiException($"Administador com id {id} não foi encontrado");

        public async Task<Administrador?> SelectByEmailAsync(string email) =>
            await dbContext.Administradores.FirstOrDefaultAsync(a => a.Email == email);

        public async Task CriarAsync(Administrador administrador)
        {
            if(await SelectByEmailAsync(administrador.Email) != null)
            {
                throw new ApiException($"Administador com email {administrador.Email} já existe");
            }

            await dbContext.AddAsync(administrador);
            await dbContext.SaveChangesAsync();
        }

        public async Task EditarAsync(Administrador administrador)
        {
            dbContext.Update(administrador);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeletarAsync(Administrador administrador)
        {
            dbContext.Remove(administrador);
            await dbContext.SaveChangesAsync();
        }
    }
}
