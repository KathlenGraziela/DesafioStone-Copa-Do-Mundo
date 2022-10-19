using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoneDesafio.Business.Repositorys;
using StoneDesafio.Business.Services;
using StoneDesafio.Controllers.Teste;
using StoneDesafio.Data.ClubeDtos;
using StoneDesafio.Entities;
using StoneDesafio.Models;

namespace StoneDesafio.Controllers
{
    public class ClubeController : GenericController<Clube, ClubeCriarDto, ClubeEditarDto>
    {
        public ClubeController(IRepository<Clube> repository, IService<Clube, ClubeCriarDto, ClubeEditarDto> service) : base(repository, service)
        {
        }
    }
}
