using System;
using System.Net;

namespace CMusicPlayer.Data.Network.Types.Exceptions
{
    internal class HttpException : Exception
    {
        public HttpStatusCode StatusCode;

        public HttpException(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}