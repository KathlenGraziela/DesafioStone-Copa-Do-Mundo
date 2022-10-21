using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoneDesafio.Business.Repositorys;
using StoneDesafio.Business.Services;
using StoneDesafio.Data.ClubeDtos;
using StoneDesafio.Data.JogoDtos;
using StoneDesafio.Data.ResultadoDtos;
using StoneDesafio.Models;

namespace StoneDesafio.Controllers
{
    [Authorize]
    public class ResultadoController : GenericController<Resultado, ResultadoCriarDto, ResultadoEditarDto>
    {
        private readonly IRepository<Jogo> jogoRepository;
        private readonly IRepository<Clube> clubeRepository;
        private readonly IRepository<FaseCampeonato> faseCampeonatoRepository;

        public ResultadoController(IRepository<Resultado> repository, IService<Resultado, ResultadoCriarDto, ResultadoEditarDto> service, IRepository<Clube> clubeRepository, IRepository<FaseCampeonato> faseCampeonatoRepository, IRepository<Jogo> jogoRepository) : base(repository, service)
        {
            this.jogoRepository = jogoRepository;
            this.clubeRepository = clubeRepository;
            this.faseCampeonatoRepository = faseCampeonatoRepository;
        }

        [AllowAnonymous]
        public override async Task<IActionResult> Index(MensagemRota<Resultado> msg = null)
        {
                var jogos = await jogoRepository.GetSet()
                .Include(j => j.ClubeA)
                .Include(j => j.ClubeA).ThenInclude(c => c.Grupo)
                .Include(j => j.ClubeB)
                .Include(j => j.ClubeB).ThenInclude(c => c.Grupo)
                .Include(j => j.Fase)
                .Include(j => j.Resultado)
                .Select(j => new JogoResultadoDto()
                {
                    Id = j.Id,
                    Fase = j.Fase.FaseAtualCampeonato.ToString(),
                    DataInicio = j.InicioJogo.ToString("dd/MM/yy hh:mm"),
                    ClubeA = new ClubeDetalheDto()
                    {
                        Id = j.ClubeA.Id,
                        Nome = j.ClubeA.Nome,
                        Grupo = j.ClubeA.Grupo.Nome
                    },
                    ClubeB = new ClubeDetalheDto()
                    {
                        Id = j.ClubeB.Id,
                        Nome = j.ClubeB.Nome,
                        Grupo = j.ClubeB.Grupo.Nome
                    },
                    Resultado =
                    j.Resultado == null ?
                    new ResultadoDetalheDto() :
                    new ResultadoDetalheDto()
                    {
                        Id = j.Resultado.Id,
                        GolsClubeA = j.Resultado.GolsClubeA.ToString(),
                        GolsClubeB = j.Resultado.GolsClubeB.ToString()
                    }
                })
                .ToListAsync();
            return View(jogos);
        }
    }
}
