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
using StoneDesafio.Entities;
using System.Net.Http.Json;
using System.Reflection;
using System.Threading.Channels;
using Xunit.Sdk;

namespace StoneDesafio.Tests
{
    public class AdministradorIntegracao
    {
        private readonly HttpClient clientTest;

        public AdministradorIntegracao()
        {
            var appFactory = new WebApplicationFactory<Program>();
            clientTest = appFactory.CreateClient();
        }


        [Fact(DisplayName = "Teste de rotas dos Administradores")]
        public async Task DeveFazerOperacoesCRUDAsync()
        {
            var rotaAtributo = (RouteAttribute?)Attribute.GetCustomAttribute(typeof(AdministradorController), typeof(RouteAttribute));
            var rotaBase = rotaAtributo.Template;


            var createDto = new AdministradorCreateDto()
            {
                Nome = "Adm",
                Email = "adm@adms.com",
                Senha = "pass"
            };
            
            var httpResponse = await clientTest.PostAsJsonAsync(rotaBase, createDto);
            var administradorCreate = await httpResponse.Content.ReadFromJsonAsync<AdministradorReadDto>() ?? throw new NullReferenceException();

            Assert.Equal(createDto.Nome, administradorCreate.Nome);
            Assert.Equal(createDto.Email, administradorCreate.Email);




            httpResponse = await clientTest.GetAsync(rotaBase + $"/{administradorCreate.Id}");
            Assert.True(httpResponse.IsSuccessStatusCode);


            var editDto = new AdministradorEditDto()
            {
                Nome = "Admas",
                Senha = "mustache"
            };


            httpResponse = await clientTest.PutAsJsonAsync(rotaBase + $"/{administradorCreate.Id}", editDto);
            var administradorUpdate = await httpResponse.Content.ReadFromJsonAsync<AdministradorReadDto>() ?? throw new NullReferenceException();

            Assert.True(httpResponse.IsSuccessStatusCode);
            Assert.Equal(editDto.Nome, administradorUpdate.Nome);


            httpResponse = await clientTest.DeleteAsync(rotaBase + $"/{administradorCreate.Id}");
            Assert.True(httpResponse.IsSuccessStatusCode);
        }
    }
}