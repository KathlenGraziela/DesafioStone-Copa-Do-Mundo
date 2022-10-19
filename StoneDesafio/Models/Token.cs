namespace StoneDesafio.Models
{
    public class Token
    {
        public string Valor { get; set; }

        public override string ToString()
        {
            return Valor;
        }
    }
}