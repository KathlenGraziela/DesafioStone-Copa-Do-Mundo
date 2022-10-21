using StoneDesafio.Enum;

namespace StoneDesafio.Services
{
    public static class FaseTradutor
    {
        public static string Traduzir(FasesCampeonato fase)
        {
            string traducao = "";
            switch (fase)
            {
                case FasesCampeonato.Grupo:
                    traducao = "de grupo";
                    break;
                case FasesCampeonato.Oitavas:
                    traducao = "oitavas";
                    break;
                case FasesCampeonato.Quarta:
                    traducao = "quarta";
                    break;
                case FasesCampeonato.SemiFinal:
                    traducao = "semi-final";
                    break;
                case FasesCampeonato.Final:
                    traducao = "final";
                    break;
            }
            return traducao;
        }
    }
}
