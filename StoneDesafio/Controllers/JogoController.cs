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

        public JogoController(IRepository<Jogo> repository, IService<Jogo, JogoCriarDto, JogoEditarDto> service, IRepository<Clube> clubeRepository) : base(repository, service)
        {
            this.clubeRepository = clubeRepository;
        }

        public override async Task<IActionResult> Criar()
        {
            var clubes = await clubeRepository.SelectAllAsync();
            ViewData["ListaClubes"] = clubes;
            return View();
        }
        [Route("indexjogo")]
        public IActionResult IndexJogo() => View();

        [Route ("ListarJogos")]
        public async Task<IActionResult> ListarJogos()
        {
            var jogos = await repository.GetSet()
                .Include(j => j.ClubeA)
                .Include(j => j.ClubeB)
                .Include(j => j.Resultado)
                .Select(j => new JogoResultadoDto()
                {
                    Id = j.Id,
                    DataInicio = j.InicioJogo.ToString("dd/MM/yy hh:mm"),
                    ClubeA = new ClubeDetalheDto()
                    {
                        Id = j.ClubeA.Id,
                        Nome = j.ClubeA.Nome
                    },
                    ClubeB = new ClubeDetalheDto()
                    {
                        Id = j.ClubeB.Id,
                        Nome = j.ClubeB.Nome
                    },
                    Resultado = 
                    j.Resultado == null ? 
                    new ResultadoDetalheDto() : 
                    new ResultadoDetalheDto()
                    {
                        Id = j.Resultado.Id,
                        GolsClubeA = j.Resultado.GolsClubeA,
                        GolsClubeB = j.Resultado.GolsClubeB
                    }
                })
                .ToListAsync();
            return View (jogos);
        }

        public override async Task<IActionResult> Index(MensagemRota<Jogo> msg = null)
        {
            var jogos = await repository.GetSet().Include(j => j.ClubeA).Include(j => j.ClubeB).ToListAsync();
            return View(jogos);
        }
    }
}
