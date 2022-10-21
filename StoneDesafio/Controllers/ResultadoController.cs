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
    }
}
