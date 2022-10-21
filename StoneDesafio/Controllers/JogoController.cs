using GenericRepositoryBuilder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoneDesafio.Business.Repositorys;
using StoneDesafio.Business.Services;
using StoneDesafio.Data.ClubeDtos;
using StoneDesafio.Data.JogoDtos;
using StoneDesafio.Data.ResultadoDtos;
using StoneDesafio.Entities;
using StoneDesafio.Models;

namespace StoneDesafio.Controllers
{
    public class JogoController : GenericController<Jogo, JogoCriarDto, JogoEditarDto>
    {
        private readonly IRepository<Clube> clubeRepository;
        private readonly IRepository<FaseCampeonato> faseCampeonatoRepository;

        public JogoController(IRepository<Jogo> repository, IService<Jogo, JogoCriarDto, JogoEditarDto> service, IRepository<Clube> clubeRepository, IRepository<FaseCampeonato> faseCampeonatoRepository) : base(repository, service)
        {
            this.clubeRepository = clubeRepository;
            this.faseCampeonatoRepository = faseCampeonatoRepository;
        }

        public override async Task<IActionResult> Index(MensagemRota<Jogo> msg = null)
        {
            var jogos = await repository.GetSet()
                .Include(j => j.ClubeA)
                .Include(j => j.ClubeB)
                .Include(j => j.Fase)
                .ToListAsync();
            return View(jogos);
        }

        public override async Task<IActionResult> Criar()
        {
            var clubes = await clubeRepository.SelectAllAsync();
            ViewData["ListaClubes"] = clubes;
            var fases = await faseCampeonatoRepository.SelectAllAsync();
            ViewData["ListaFases"] = fases;
            return View();
        }

        [Route ("ListarJogos")]
        public async Task<IActionResult> ListarJogos()
        {
            var jogos = await repository.GetSet()
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
            return View (jogos);
        }

        public override async Task<IActionResult> Editar(int id)
        {
            var jogo = await repository.GetSet()
                .Include(j => j.ClubeA)
                .Include(j => j.ClubeB)
                .FirstOrDefaultAsync(j => j.Id == id);

            var clubes = await clubeRepository.SelectAllAsync();
            ViewData["ListaClubes"] = clubes;
            if (jogo == null)
            {
                var msg = new MensagemRota<Clube>(MensagemResultado.Falha, $"Clube nao encontrado!");
                return RedirectToAction(nameof(Index), msg);
            }
            return View(jogo);
        }
    }
}
