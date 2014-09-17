using System;
using Starscream.Domain.Services;
using Starscream.Notifications;

namespace Starscream.Domain
{
    public class VisitorSession : IUserSession
    {
        #region IUserSession Members

        public Guid Id { get; private set; }

        #endregion
    }
}