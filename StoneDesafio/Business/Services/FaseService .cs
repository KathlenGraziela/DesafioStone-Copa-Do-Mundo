using StoneDesafio.Business.Repositorys;
using StoneDesafio.Data;
using StoneDesafio.Entities;
using StoneDesafio.Models;
using StoneDesafio.Models.Utils;

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
            if (await faseRepository.FindFirstAsync(a => a.FaseAtualCampeonato == createDto.FasesCampeonato) != null)
            {
                throw new Exception($"Fase {createDto.FasesCampeonato} já existe");
            }

            var fase = new FaseCampeonato
            {
                FaseAtualCampeonato = createDto.FasesCampeonato,
            };

            await faseRepository.AddAndSaveAsync(fase);

            return fase;
        }

        public async Task<FaseCampeonato> EditarAsync(int id, FaseEditarDto editDto)
        {
            var fase = await faseRepository.FindFirstAsync(a => a.Id == id)  ??
                throw new Exception($"Fase com id {id} não foi encontrado"); 

            modelConverter.ConvertInPlace(editDto, fase, checkNull: true);

            await faseRepository.UpdateAndSaveAsync(fase);

            return fase;
        }

        public async Task DeletarAsync(int id)
        {
            var administrador = await faseRepository.FindFirstAsync(a => a.Id == id) ??
                    throw new Exception($"Fase com id {id} não foi encontrado");

            await faseRepository.RemoveAndSaveAsync(administrador);
        }
    }
}
