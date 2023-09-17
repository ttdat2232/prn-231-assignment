using Application.Exceptions.Base;
using System.Net;

namespace Application.Exceptions
{
    public class ForbidenException : BaseException
    {
        private readonly static int HTTP_STATUS = (int)HttpStatusCode.Forbidden;
        public ForbidenException(string message) : base(message, HTTP_STATUS) { }
    }
}
