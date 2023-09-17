using Application.Exceptions.Base;
using System.Net;
using System.Text;

namespace Application.Exceptions
{
    public class NotFoundException<T> : BaseException where T : class
    {
        private readonly static int HTTP_STATUS = (int)HttpStatusCode.NotFound;

        public NotFoundException() : base() { }

        public NotFoundException(object keySelector, Type? classThrowException = null) : base($"{classThrowException?.Name + ": "}{typeof(T).Name}#{GetKey(keySelector)} is not found", HTTP_STATUS)
        {
        }

        private static string GetKey(object keySelector)
        {
            if (keySelector is Array)
            {
                var message = new StringBuilder();
                var array = (Array)keySelector;
                foreach (object key in array)
                {
                    message.Append(key.ToString()).Append(" ");
                }
                return message.ToString();
            }
            return keySelector.ToString() ?? "";
        }
    }
}
