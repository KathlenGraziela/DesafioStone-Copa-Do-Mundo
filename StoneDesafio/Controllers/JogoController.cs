using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoneDesafio.Business.Repositorys;
using StoneDesafio.Business.Services;
using StoneDesafio.Data.ClubeDtos;
using StoneDesafio.Data.JogoDtos;
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
    }
}
