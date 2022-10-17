using Microsoft.AspNetCore.Mvc;
using StoneDesafio.Business.Repositorys;
using StoneDesafio.Business.Services;
using StoneDesafio.Data.AdministradorDtos;
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

            return adiministradores.ConvertAll(a => modelConverter.Convert<AdministradorReadDto, Administrador>(a));
        }

        [HttpGet("{id}")]
        public async Task<AdministradorReadDto> PegarUmAsync(Guid id)
        {
            var adiministrador = await administradorRepository.FindAsync(id);
            return modelConverter.Convert<AdministradorReadDto, object>(adiministrador);
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