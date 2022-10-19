using StoneDesafio.Business.Repositorys;
using StoneDesafio.Controllers;
using StoneDesafio.Data.AdministradorDtos;
using StoneDesafio.Models;

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

        public async Task<MensagemRota<Token>> Login(AdministradorLoginDto login)
        {
            var administrador = await repository.FindFirstAsync(a => a.Email == login.Email);
            if (administrador == null || !CriptografiaService.Verficar(login.Senha, administrador.Senha))
            {
                return new(MensagemResultado.Falha, "Usuário e/ou Senha inválidos. Por favor, tente novamente.");
            }
            //Faca um token aqui!
            return new(MensagemResultado.Sucesso, null, new Token() { Valor = "tokenzinho" });
        }

        public async Task<MensagemRota<Token>> Cadastrar(AdministradorCriarDto createDto)
        {
            var result = await administradorService.CriarAsync(createDto);
            if(result.Resultado != MensagemResultado.Sucesso)
            {
                return new(result.Resultado, result.Mensagem);
            }
            //Faca um token aqui!
            return new(MensagemResultado.Sucesso, null, new Token() { Valor = "tokenzinho" });
        }
    }
}
