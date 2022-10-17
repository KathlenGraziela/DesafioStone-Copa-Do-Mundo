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

namespace StoneDesafio.Tests.Administrador
{
    public class IntegracaoAdministrador
    {
        private readonly WebApplicationFactory<Program> applicationFactory;
        public IntegracaoAdministrador()
        {
            applicationFactory = new WebApplicationFactory<Program>();
        }

        [Fact(Skip = "Skip")]
        public async Task Test1Async()
        {
            var services = new ServiceCollection()
                .AddTransient<AdministradorController>()
                .BuildServiceProvider()
                .GetRequiredService<AdministradorController>();

            //var administradorController = applicationFactory.Services.GetRequiredService<AdministradorController>();
            //var result = await administradorController.CriarAsync(new() { Nome = "Teste", Email = "Testando@t.com", Senha = "1234" });
            //Assert.NotNull(result);
        }
    }
}