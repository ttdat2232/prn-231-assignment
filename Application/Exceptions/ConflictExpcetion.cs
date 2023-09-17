using Application.Exceptions.Base;
using System.Net;

namespace Application.Exceptions
{
    public class ConflictExpcetion : BaseException
    {
        private static readonly int HTTP_STATUS = (int)HttpStatusCode.BadRequest;

        public ConflictExpcetion(string message) : base(message, HTTP_STATUS) { }
    }
}
