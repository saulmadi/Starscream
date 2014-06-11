using System;
using IvoryTower.Domain;

namespace IvoryTower.Web.Api.Infrastructure.Authentication
{
    public class VisitorSession : IUserSession
    {
        #region IUserSession Members

        public Guid Id { get; private set; }

        #endregion
    }
}