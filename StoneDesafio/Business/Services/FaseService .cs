using GenericRepositoryBuilder;
using Microsoft.EntityFrameworkCore;
using StoneDesafio.Business.Repositorys;
using StoneDesafio.Data.FaseDtos;
using StoneDesafio.Entities;
using StoneDesafio.Models;
using StoneDesafio.Models.Utils;

namespace StoneDesafio.Business.Services
{
    public class FaseService
    {
        private readonly ModelConverter modelConverter;
        private readonly IRepository<FaseCampeonato> faseRepository;


        public FaseService(ModelConverter modelConverter, IRepository<FaseCampeonato> genericRepository)
        {
            this.modelConverter = modelConverter;
            this.faseRepository = genericRepository;
        }

        public async Task<MensagemRota<FaseCampeonato>> CriarAsync(FaseCriarDto criarDto)
        {
            if (await faseRepository.FindFirstAsync(a => a.FaseAtualCampeonato == criarDto.FaseAtualCampeonato) != null)
            {
                return new(MensagemResultado.Falha, "Essa fase ja existe!");
            }

            var fase = new FaseCampeonato
            {
                FaseAtualCampeonato = criarDto.FaseAtualCampeonato,
            };

            await faseRepository.AddAndSaveAsync(fase);

            return new(MensagemResultado.Sucesso, "Fase criada com sucesso!", fase);
        }

        public async Task<MensagemRota<FaseCampeonato>> DeletarAsync(int id)
        {
            var fase = await faseRepository.FindAsync(id);
            if (fase == null)
            {
                return new(MensagemResultado.Falha, "Fase nao encontrada!");
            }

            await faseRepository.RemoveAndSaveAsync(fase);
            return new(MensagemResultado.Sucesso, "Fase deletada com sucesso!");
        }
    }
}
