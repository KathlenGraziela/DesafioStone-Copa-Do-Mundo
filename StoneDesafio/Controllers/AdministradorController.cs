using Microsoft.AspNetCore.Mvc;
using StoneDesafio.Business.Repositorys;
using StoneDesafio.Business.Services;
using StoneDesafio.Data.AdministradorDtos;
using StoneDesafio.Models;
using StoneDesafio.Models.Utils;

namespace StoneDesafio.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class AdministradorController : AppBaseController
    {
        private readonly AdministradorService administradorService;
        private readonly ModelConverter modelConverter;
        private readonly IRepository<Administrador> administradorRepository;

        public AdministradorController(ModelConverter modelConverter, AdministradorService administradorService, IRepository<Administrador> administradorRepository)
        {
            this.modelConverter = modelConverter;
            this.administradorService = administradorService;
            this.administradorRepository = administradorRepository;
        }

        [HttpGet]
        public async Task<List<AdministradorReadDto>> PegarListaAsync(int? n)
        {
            var adiministradores =  n == null ? await administradorRepository.SelectAllAsync() 
                                              : await administradorRepository.SelectNAsync((int) n);

            return adiministradores.ConvertAll(a => modelConverter.Convert<AdministradorReadDto>(a));
        }

        [HttpGet("{id}")]
        public async Task<AdministradorReadDto> PegarUmAsync(int id)
        {
            var adiministrador = await administradorRepository.FindAsync(id);
            return modelConverter.Convert<AdministradorReadDto>(adiministrador);
        }

        [HttpPost]
        public async Task<AdministradorReadDto> CriarAsync([FromBody] AdministradorCriarDto createDto)
        {
            var adiministrador = await administradorService.CriarAsync(createDto);
            return modelConverter.Convert<AdministradorReadDto>(adiministrador);
        }

        [HttpPut("{id}")]
        public async Task<AdministradorReadDto> EditarAsync(int id, [FromBody] AdministradorEditarDto editDto)
        {
            var administrador = await administradorService.EditarAsync(id, editDto);
            return modelConverter.Convert<AdministradorReadDto>(administrador);
        }

        [HttpDelete("{id}")]
        public async Task DeletarAsync(int id)
        {
            await administradorService.DeletarAsync(id);
        }
    }
}