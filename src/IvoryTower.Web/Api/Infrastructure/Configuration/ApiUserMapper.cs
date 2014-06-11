using System;
using IvoryTower.Data;
using IvoryTower.Domain;
using IvoryTower.Web.Api.Infrastructure.Authentication;
using Nancy.Security;

namespace IvoryTower.Web.Api.Infrastructure.Configuration
{
    public class ApiUserMapper : IApiUserMapper<Guid>
    {
        readonly IReadOnlyRepository _readOnlyRepo;
        readonly ITimeProvider _timeProvider;

        public ApiUserMapper(IReadOnlyRepository readOnlyRepo, ITimeProvider timeProvider)
        {
            _readOnlyRepo = readOnlyRepo;
            _timeProvider = timeProvider;
        }

        #region IApiUserMapper<Guid> Members

        public IUserIdentity GetUserFromAccessToken(Guid token)
        {
            UserSession userSession = GetUserSessionFromToken(token);
            MakeSureTokenHasntExpiredYet(userSession);
            return new IvoryTowerUserIdentity(userSession);
        }

        #endregion

        UserSession GetUserSessionFromToken(Guid token)
        {
            UserSession userSession;
            try
            {
                userSession = _readOnlyRepo.First<UserSession>(x => x.Id == token);
            }
            catch (ItemNotFoundException<UserSession> e)
            {
                throw new TokenDoesNotExistException();
            }
            return userSession;
        }

        void MakeSureTokenHasntExpiredYet(UserSession userSession)
        {
            DateTime expires = userSession.Expires;
            DateTime now = _timeProvider.Now();
            if (expires < now)
            {
                throw new TokenExpiredException();
            }
        }
    }
}