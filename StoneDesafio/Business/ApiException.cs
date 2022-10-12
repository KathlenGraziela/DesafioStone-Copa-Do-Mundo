
namespace StoneDesafio.Businesss
{
    public class ApiException : Exception { 
        public ApiException(string error) : base(error) { }
        
    }
}
