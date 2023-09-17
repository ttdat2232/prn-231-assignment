namespace Application.Exceptions.Base
{
    public class BaseException : Exception
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        
        public BaseException()
        {
        }

        public BaseException(string message, int statusCode = 500) : base(message) 
        { 
            StatusCode = statusCode;
            ErrorMessage = message;
        }
    }
}
