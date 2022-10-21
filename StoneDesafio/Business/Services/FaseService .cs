﻿using GenericRepositoryBuilder;
using StoneDesafio.Business.Repositorys;
using StoneDesafio.Data.FaseDtos;
using StoneDesafio.Entities;
using StoneDesafio.Models;
using StoneDesafio.Models.Utils;

namespace StoneDesafio.Business.Services
{
    public class FaseService : IService<FaseCampeonato, FaseCriarDto, FaseEditarDto>
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

        public async Task<FaseCampeonato> EditarAsync(int id, FaseEditarDto editDto)
        {
            var fase = await faseRepository.FindFirstAsync(a => a.Id == id)  ??
                throw new Exception($"Fase com id {id} não foi encontrado"); 

            modelConverter.ConvertInPlace(editDto, fase, checkNull: true);

            await faseRepository.UpdateAndSaveAsync(fase);

            return fase;
        }

        async Task<MensagemRota<FaseCampeonato>> IService<FaseCampeonato, FaseCriarDto, FaseEditarDto>.CriarAsync(FaseCriarDto criarDto)
        {
            if (await faseRepository.FindFirstAsync(a => a.FaseAtualCampeonato == criarDto.FasesCampeonato) != null)
            {
                return new(MensagemResultado.Falha, "Essa fase ja existe!");
            }

            var fase = new FaseCampeonato
            {
                FaseAtualCampeonato = criarDto.FasesCampeonato,
                Jogos = await jogoRepository.SelectWhereAsync(j => criarDto.Jogos.Contains(j.Id)),
            };

            await faseRepository.AddAndSaveAsync(fase);

            return new(MensagemResultado.Sucesso, "Fase criada com sucesso!", fase);
        }

        async Task<MensagemRota<FaseCampeonato>> IService<FaseCampeonato, FaseCriarDto, FaseEditarDto>.EditarAsync(FaseEditarDto editarDto)
        {
            var fase = await faseRepository.FindFirstAsync(f => f.Id == editarDto.Id);
            if (fase == null)
            {
                return new(MensagemResultado.Falha, $"Fase com id {fase.Id} não foi encontrada!");
            }


            fase.Jogos = await jogoRepository.SelectWhereAsync(j => editarDto.Jogos.Contains(j.Id));

            await faseRepository.UpdateAndSaveAsync(fase);

            return new(MensagemResultado.Sucesso, "Fase editada com sucesso!", fase);
        }

        async Task<MensagemRota<FaseCampeonato>> IService<FaseCampeonato, FaseCriarDto, FaseEditarDto>.DeletarAsync(int id)
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
