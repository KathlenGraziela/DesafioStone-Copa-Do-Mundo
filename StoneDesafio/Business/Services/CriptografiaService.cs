using System.Text;
using static BCrypt.Net.BCrypt;

namespace StoneDesafio.Business.Services
{
    public static class CriptografiaService
    {
        private static readonly string saltBcrypt = 
            Convert.ToBase64String(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("saltDB") ?? "salter"));

        internal static readonly string saltJWT = Environment.GetEnvironmentVariable("saltJWT") ?? "pass";
        public static string Criptografar(string senha) =>
            HashPassword(senha + saltBcrypt, 11);

        public static bool Verficar(string senha, string hash) =>
            Verify(senha + saltBcrypt, hash);
    }
}
