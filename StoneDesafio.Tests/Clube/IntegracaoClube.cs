using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using StoneDesafio.Business.Repositorys;
using StoneDesafio.Business.Services;
using StoneDesafio.Controllers;
using StoneDesafio.Data;
using StoneDesafio.Data.AdministradorDtos;
using StoneDesafio.Data.ClubeDtos;
using StoneDesafio.Entities;
using StoneDesafio.Models;
using System.Net.Http.Json;
using System.Reflection;
using System.Threading.Channels;
using Xunit.Sdk;

namespace StoneDesafio.Tests.ClubeTests
{
    public class IntegracaoClube : IClassFixture<WebTestFixture>
    {
        private readonly IService<Clube, ClubeCriarDto, ClubeEditarDto> clubeService;
        private readonly IRepository<Clube> clubeRepository;

        public IntegracaoClube(WebTestFixture webTestFixture)
        {
            var serviceProvider = webTestFixture.ServiceProvider;
            clubeService = serviceProvider.GetRequiredService<IService<Clube, ClubeCriarDto, ClubeEditarDto>>();
            clubeRepository = serviceProvider.GetRequiredService<IRepository<Clube>>();
        }

        [Fact]
        public async Task DeveFazerCRUDClube()
        {
            var criarDto = new ClubeCriarDto() { Nome = "Teste", Descricao = "algo", UrlFoto = "url" };
            var resultCriar = await clubeService.CriarAsync(criarDto);

            Assert.True(resultCriar.Resultado == MensagemResultado.Sucesso);

            Assert.Equal(resultCriar.Sucessos.Nome, criarDto.Nome);
            Assert.Equal(resultCriar.Sucessos.Descricao, criarDto.Descricao);
            Assert.Equal(resultCriar.Sucessos.UrlFoto, criarDto.UrlFoto);

            var editarDto = new ClubeEditarDto() { Id = resultCriar.Sucessos.Id, Nome = "Editado", Descricao = "outro", UrlFoto = "zz" };

            var resultadoEditar = await clubeService.EditarAsync(editarDto);

            Assert.True(resultCriar.Resultado == MensagemResultado.Sucesso);

            var clube = await clubeRepository.FindAsync(editarDto.Id);

            Assert.Equal(clube.Nome, editarDto.Nome);
            Assert.Equal(clube.Descricao, editarDto.Descricao);
            Assert.Equal(clube.UrlFoto, editarDto.UrlFoto);

            var resultadoDeletar = await clubeService.DeletarAsync(editarDto.Id);

            Assert.True(resultadoDeletar.Resultado == MensagemResultado.Sucesso);
        }
    }
}