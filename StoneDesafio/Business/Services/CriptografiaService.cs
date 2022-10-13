using System.Text;
using static BCrypt.Net.BCrypt;

namespace StoneDesafio.Business.Services
{
    public static class CriptografiaService
    {
        private static readonly string salt = Convert.ToBase64String(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("saltDB") ?? "salter"));
       
        public static string Criptografar(string senha) =>
            HashPassword(senha + salt, 11);

        public static bool Verficar(string senha, string hash) =>
            Verify(senha + salt, hash);
    }
}
