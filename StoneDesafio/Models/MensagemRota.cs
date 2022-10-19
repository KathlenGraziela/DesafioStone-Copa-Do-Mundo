namespace StoneDesafio.Models
{
    public enum MensagemResultado
    {
        Sucesso,
        Falha,
    }

    public class MensagemRota<T> where T : class
    {
        public string Mensagem { get; set; }
        public MensagemResultado Resultado { get; set; }

        public T Sucessos { get; set; }

        public MensagemRota(MensagemResultado resultado, string mensagem, T sucessos = null)
        {
            Resultado = resultado;
            Mensagem = mensagem;
            Sucessos = sucessos;
        }

        public MensagemRota() { }

        public override string ToString()
        {
            return Mensagem;
        }
    }
}
