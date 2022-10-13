using StoneDesafio.Business.Services;

namespace StoneDesafio.Tests.Administrador
{
    public class UnidadeAdministrador
    {
        [Fact]
        public void CriptografarSenha()
        {
            var senha = "password";

            var result = CriptografiaService.Criptografar(senha);

            Assert.NotEqual(senha, result);

            Assert.True(CriptografiaService.Verficar(senha, result));
        }
    }
}