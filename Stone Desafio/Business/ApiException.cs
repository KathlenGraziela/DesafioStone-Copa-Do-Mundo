
namespace Stone_Desafio.Businesss
{
    public class ApiException : Exception { 
        public ApiException(string error) : base(error) { }
        
    }
}
