using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoneDesafio.Business.Repositorys;
using StoneDesafio.Business.Services;
using StoneDesafio.Data.ClubeDtos;
using StoneDesafio.Entities;
using StoneDesafio.Models;

namespace StoneDesafio.Controllers
{
    [Authorize]
    public class ClubeController : GenericController<Clube, ClubeCriarDto, ClubeEditarDto>
    {
        private readonly IRepository<Grupo> grupoRepository;

        public ClubeController(IRepository<Clube> repository, IService<Clube, ClubeCriarDto, ClubeEditarDto> service, IRepository<Grupo> grupoRepository) : base(repository, service)
        {
            this.grupoRepository = grupoRepository;
        }
        public override async Task<IActionResult> Criar()
        {
            var grupo = await grupoRepository.SelectAllAsync();
            ViewData["ListaGrupo"] = grupo;
  
            return View();
        }
    }
}
