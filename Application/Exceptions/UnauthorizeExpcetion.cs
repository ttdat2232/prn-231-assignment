using Application.Exceptions.Base;
using System.Net;

namespace Application.Exceptions
{
    public class UnauthorizeExpcetion : BaseException
    {
        private readonly static int HTTP_STATUS = (int)HttpStatusCode.Unauthorized;

        public UnauthorizeExpcetion(string message) : base(message, HTTP_STATUS)
        {
        }
    }
}
