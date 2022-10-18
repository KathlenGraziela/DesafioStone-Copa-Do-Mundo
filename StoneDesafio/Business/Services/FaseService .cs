using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Utilities.Encoders;
using StoneDesafio.Business.Repositorys;
using StoneDesafio.Businesss;
using StoneDesafio.Data;
using StoneDesafio.Data.AdministradorDtos;
using StoneDesafio.Entities;
using StoneDesafio.Models;
using StoneDesafio.Models.Utils;
using System.Text;

namespace StoneDesafio.Business.Services
{
    public class FaseService
    {
        private readonly AppDbContext dbContext;
        private readonly ModelConverter modelConverter;
        private readonly IRepository<FaseCampeonato> faseRepository;
        private readonly IRepository<Jogo> jogoRepository;


        public FaseService(AppDbContext dbContext, ModelConverter modelConverter, IRepository<FaseCampeonato> genericRepository, IRepository<Jogo> jogoRepository)
        {
            this.dbContext = dbContext;
            this.modelConverter = modelConverter;
            this.faseRepository = genericRepository;
            this.jogoRepository = jogoRepository;
        }

        public async Task<FaseCampeonato> CriarAsync(FaseCriarDto createDto)
        {
            if (await faseRepository.SelectFirstAsync(a => a.FasesCampeonato == createDto.FasesCampeonato) != null)
            {
                throw new ApiException($"Fase {createDto.FasesCampeonato} já existe");
            }
            var jogos = await jogoRepository.SelectWhereAsync(j => createDto.Jogos.Contains(j.Id));

            var fase = new FaseCampeonato
            {
                Id = Guid.NewGuid(),
                FasesCampeonato = createDto.FasesCampeonato,
                Jogos = jogos
            };

            await faseRepository.AddAndSaveAsync(fase);

            return fase;
        }

        public async Task<FaseCampeonato> EditarAsync(Guid id, FaseEditarDto editDto)
        {
            var fase = await faseRepository.SelectFirstAsync(a => a.Id == id)  ??
                throw new ApiException($"Fase com id {id} não foi encontrado"); 

            modelConverter.ConvertInPlace(editDto, fase, checkNull: true);

            await faseRepository.UpdateAndSaveAsync(fase);

            return fase;
        }

        public async Task DeletarAsync(Guid id)
        {
            var administrador = await faseRepository.SelectFirstAsync(a => a.Id == id) ??
                    throw new ApiException($"Fase com id {id} não foi encontrado");

            await faseRepository.RemoveAndSaveAsync(administrador);
        }
    }
}
