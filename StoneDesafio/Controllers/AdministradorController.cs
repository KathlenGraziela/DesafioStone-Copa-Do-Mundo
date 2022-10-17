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
    public class AdministradorController : ControllerBase
    {
        private readonly AdministradorService administradorService;
        private readonly ModelConverter modelConverter;
        private readonly IAdministradorRepository genericRepository;

        public AdministradorController(ModelConverter modelConverter, AdministradorService administradorService, IAdministradorRepository genericRepository)
        {
            this.modelConverter = modelConverter;
            this.administradorService = administradorService;
            this.genericRepository = genericRepository;
        }

        [HttpGet]
        public async Task<List<AdministradorReadDto>> PegarListaAsync(int? n)
        {
            var adiministradores =  n == null ? await genericRepository.SelectAllAsync() 
                                              : await genericRepository.SelectNAsync((int) n);

            return adiministradores.ConvertAll(a => modelConverter.Convert<AdministradorReadDto, Administrador>(a));
        }

        [HttpGet("{id}")]
        public async Task<AdministradorReadDto> PegarUmAsync(Guid id)
        {
            var adiministrador = await genericRepository.SelectFirstAsync(a => a.Id == id);
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
            var administrador = await administradorService.EditarAsync(id, editDto);
            return modelConverter.Convert<AdministradorReadDto, Administrador>(administrador);
        }

        [HttpDelete("{id}")]
        public async Task DeletarAsync(Guid id)
        {
            await administradorService.DeletarAsync(id);
        }

    }
}