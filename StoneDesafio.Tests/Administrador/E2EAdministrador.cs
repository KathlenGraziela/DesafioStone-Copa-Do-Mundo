

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using StoneDesafio.Controllers;
using StoneDesafio.Data.AdministradorDtos;
using System.Net.Http.Json;

namespace StoneDesafio.Tests.Administrador
{
    public class E2EAdministrador
    {
        //private readonly WebApplicationFactory<Program> appFactory = new();
        //private readonly HttpClient clientTest;
        //private readonly string rotaBase;

        //public E2EAdministrador()
        //{
        //    clientTest = appFactory.CreateClient();

        //    var rotaAtributo = (RouteAttribute?)Attribute.GetCustomAttribute(typeof(AdministradorController), typeof(RouteAttribute));
        //    rotaBase = rotaAtributo.Template;
        //}


        //[Fact(DisplayName = "Teste de CRUD usando as rotas dos Administradores")]
        //public async Task DeveFazerCRUDComRotasAsync()
        //{
        //    var createDto = new AdministradorCriarDto()
        //    {
        //        Nome = "Adm",
        //        Email = "adm@adms.com",
        //        Senha = "pass"
        //    };

        //    var httpResponse = await clientTest.PostAsJsonAsync(rotaBase, createDto);
        //    var administradorCreate = await httpResponse.Content.ReadFromJsonAsync<AdministradorReadDto>() ?? throw new NullReferenceException();

        //    Assert.Equal(createDto.Nome, administradorCreate.Nome);
        //    Assert.Equal(createDto.Email, administradorCreate.Email);


        //    httpResponse = await clientTest.PostAsJsonAsync(rotaBase, createDto);
        //    Assert.False(httpResponse.IsSuccessStatusCode);


        //    var rotaId = rotaBase + $"/{administradorCreate.Id}";

        //    httpResponse = await clientTest.GetAsync(rotaId);
        //    Assert.True(httpResponse.IsSuccessStatusCode);


        //    var editDto = new AdministradorEditarDto()
        //    {
        //        Nome = "Admas",
        //        Senha = "mustache"
        //    };


        //    httpResponse = await clientTest.PutAsJsonAsync(rotaId, editDto);
        //    var administradorUpdate = await httpResponse.Content.ReadFromJsonAsync<AdministradorReadDto>() ?? throw new NullReferenceException();

        //    Assert.True(httpResponse.IsSuccessStatusCode);
        //    Assert.Equal(editDto.Nome, administradorUpdate.Nome);


        //    httpResponse = await clientTest.DeleteAsync(rotaId);
        //    Assert.True(httpResponse.IsSuccessStatusCode);
        //}

        
    }
}
