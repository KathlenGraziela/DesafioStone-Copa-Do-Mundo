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
using StoneDesafio.Entities;
using StoneDesafio.Models;
using System.Net.Http.Json;
using System.Reflection;
using System.Threading.Channels;
using Xunit.Sdk;

namespace StoneDesafio.Tests.Administrador
{
    public class IntegracaoAdministrador : IClassFixture<WebTestFixture>
    {
        private readonly LoginService loginService;

        public IntegracaoAdministrador(WebTestFixture webTestFixture)
        {
            var serviceProvider = webTestFixture.ServiceProvider;
            loginService = serviceProvider.GetRequiredService<LoginService>();
        }

        [Fact]
        public async Task DeveCadastrarELogarAdministrador()
        {
            var criarDto = new AdministradorCriarDto() { Nome = "Teste", Email = "Testando@t.com", Senha = "1234" };
            var result = await loginService.Cadastrar(criarDto);
            Assert.True(result.Resultado == MensagemResultado.Sucesso);

            var loginDto = new AdministradorLoginDto() { Email = criarDto.Email, Senha = criarDto.Senha };
            result = await loginService.Login(loginDto);
            Assert.True(result.Resultado == MensagemResultado.Sucesso);
        }
    }
}