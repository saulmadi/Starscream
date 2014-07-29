using System;
using Starscream.Domain;
using Starscream.Domain.Services;

namespace Starscream.Web.Api.Infrastructure.Authentication
{
    public class VisitorSession : IUserSession
    {
        #region IUserSession Members

        public Guid Id { get; private set; }

        #endregion
    }
}