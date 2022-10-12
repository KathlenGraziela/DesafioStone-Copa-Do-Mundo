using Microsoft.AspNetCore.Mvc;
using StoneDesafio.Business.Repositorys;
using StoneDesafio.Business.Services;
using StoneDesafio.Data;
using StoneDesafio.Models;
using StoneDesafio.Models.Utils;

namespace StoneDesafio.Controllers
{
    [ApiController]
    [Route("administradores")]
    [Produces("application/json")]
    public class AdministradoresController : ControllerBase
    {
        private readonly AdministradorRepository administradorRepository;
        private readonly AdministradorService administradorService;
        private readonly ModelConverter modelConverter;

        public AdministradoresController(AdministradorRepository administradorRepository, ModelConverter modelConverter, AdministradorService administradorService)
        {
            this.administradorRepository = administradorRepository;
            this.modelConverter = modelConverter;
            this.administradorService = administradorService;
        }

        [HttpGet]
        public async Task<List<AdministradorReadDto>> PegarTodosAsync()
        {
            var adiministradores = await administradorRepository.SelectTopNAsync();
            return adiministradores.ConvertAll(a => modelConverter.Convert<AdministradorReadDto, Administrador>(a));
        }

        [HttpGet("{id}")]
        public async Task<AdministradorReadDto> PegarUmAsync(Guid id)
        {
            var adiministrador = await administradorRepository.SelectByIdRequiredAsync(id);
            return modelConverter.Convert<AdministradorReadDto, Administrador>(adiministrador);
        }

        [HttpPost]
        public async Task<AdministradorReadDto> CriarAsync([FromBody] AdministradorCreateDto createDto)
        {
            var adiministrador = await administradorService.CriarAsync(createDto);
            return modelConverter.Convert<AdministradorReadDto, Administrador>(adiministrador);
        }

        [HttpPut("{id}")]
        public async Task<AdministradorReadDto> EditarAsync(Guid id, [FromBody] AdministradorEditDto editDto)
        {
            var adiministrador = await administradorService.EditarAsync(id, editDto);
            return modelConverter.Convert<AdministradorReadDto, Administrador>(adiministrador);
        }

        [HttpDelete("{id}")]
        public async Task DeletarAsync(Guid id)
        {
            var administrador = await administradorRepository.SelectByIdRequiredAsync(id);
            await administradorRepository.DeletarAsync(administrador);
        }

    }
}