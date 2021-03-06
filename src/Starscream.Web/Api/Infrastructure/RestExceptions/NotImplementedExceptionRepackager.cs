using System;
using Nancy;

namespace Starscream.Web.Api.Infrastructure.RestExceptions
{
    public class NotImplementedExceptionRepackager : IExceptionRepackager
    {
        #region IErrorHandler Members

        public ErrorResponse Repackage(Exception exception, NancyContext context, string contentType)
        {
            return new ErrorResponse(exception.Message, HttpStatusCode.NotImplemented, contentType);
        }

        public bool CanHandle(Exception ex, string contentType)
        {
            return ex is NotImplementedException;
        }

        #endregion
    }
}