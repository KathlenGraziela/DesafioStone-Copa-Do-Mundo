using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StoneDesafio.Business.Repositorys;
using StoneDesafio.Business.Services;
using StoneDesafio.Data;
using StoneDesafio.Entities;
using System.Threading.Channels;
using Xunit.Sdk;

namespace StoneDesafio.Tests
{
    public class AdministradorIntegracao
    {
        private readonly AppDbContext context;
        private readonly AdministradorRepository administradorRepository;
        private readonly AdministradorService administradorService;

        public AdministradorIntegracao()
        {

            var servico = new ServiceCollection();

            var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__MySqlConnectionString") ?? throw new Exception();
            servico.AddDbContext<AppDbContext>(opt => opt.UseMySQL(connectionString));

            context = servico.BuildServiceProvider().GetService<AppDbContext>() ?? throw new Exception();
            context.Database.EnsureCreated();

            administradorRepository = new(context);
            administradorService = new(context, administradorRepository, new());
        }


        [Fact(DisplayName = "Teste de CRUD dos Administradores")]
        public async Task DeveFazerOperacoesCRUDAsync()
        {
            var createDto = new AdministradorCreateDto()
            {
                Nome = "Adm",
                Email = "adm@adms.com",
                Senha = "pass"
            };

            var administradorCreate = await administradorService.CriarAsync(createDto);

            Assert.Equal(createDto.Nome, administradorCreate.Nome);
            Assert.Equal(createDto.Email, administradorCreate.Email);
            Assert.NotEqual(createDto.Senha, administradorCreate.Senha);

            
            var administradorRead = await administradorRepository.SelectByIdAsync(administradorCreate.Id);
            Assert.NotNull(administradorRead);

            
            var editDto = new AdministradorEditDto()
            {
                Nome = "Admas",
                Senha = "mustache"
            };


            var administradorUpdate = await administradorService.EditarAsync(administradorRead.Id, editDto);

            Assert.Equal(editDto.Nome, administradorUpdate.Nome);
            Assert.NotEqual(editDto.Senha, administradorCreate.Senha);
            Assert.NotEqual(editDto.Senha, administradorUpdate.Senha);


            await administradorRepository.DeletarAsync(administradorUpdate);
        }
    }
}