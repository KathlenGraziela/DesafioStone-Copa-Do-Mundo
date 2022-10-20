using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using MySqlX.XDevAPI.Common;
using StoneDesafio.Business.Repositorys;
using StoneDesafio.Controllers;
using StoneDesafio.Data.AdministradorDtos;
using StoneDesafio.Models;
using System.Security.Claims;

namespace StoneDesafio.Business.Services
{
    public class LoginService
    {
        private readonly IRepository<Administrador> repository;
        private readonly AdministradorService administradorService;

        public LoginService(IRepository<Administrador> repository, AdministradorService administradorService)
        {
            this.repository = repository;
            this.administradorService = administradorService;
        }

        public async Task<MensagemRota<ClaimsPrincipal>> Login(AdministradorLoginDto login)
        {
            var administrador = await repository.FindFirstAsync(a => a.Email == login.Email);
            if (administrador == null || !CriptografiaService.Verficar(login.Senha, administrador.Senha))
            {
                return new(MensagemResultado.Falha, "Usuário e/ou Senha inválidos. Por favor, tente novamente.");
            }
            return new(MensagemResultado.Sucesso, null, PegarClaim(administrador));
        }

        private static ClaimsPrincipal PegarClaim(Administrador administrador)
        {
            IEnumerable<Claim> claims = new Claim[]
            {
                new Claim("id", administrador.Id.ToString())
            };

            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
            return claimsPrincipal;
        }

        public async Task<MensagemRota<ClaimsPrincipal>> Cadastrar(AdministradorCriarDto createDto)
        {
            var result = await administradorService.CriarAsync(createDto);
            if(result.Resultado != MensagemResultado.Sucesso)
            {
                return new(result.Resultado, result.Mensagem);
            }
            return new(MensagemResultado.Sucesso, null, PegarClaim(result.Sucessos));
        }
    }
}
