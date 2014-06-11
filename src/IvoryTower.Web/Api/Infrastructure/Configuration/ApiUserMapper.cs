using System;
using IvoryTower.Data;
using IvoryTower.Domain;
using IvoryTower.Domain.Entities;
using IvoryTower.Domain.Services;
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
            UserLoginSession userLoginSession = GetUserSessionFromToken(token);
            MakeSureTokenHasntExpiredYet(userLoginSession);
            return new IvoryTowerUserIdentity(userLoginSession);
        }

        #endregion

        UserLoginSession GetUserSessionFromToken(Guid token)
        {
            UserLoginSession userLoginSession;
            try
            {
                userLoginSession = _readOnlyRepo.First<UserLoginSession>(x => x.Id == token);
            }
            catch (ItemNotFoundException<UserLoginSession> e)
            {
                throw new TokenDoesNotExistException();
            }
            return userLoginSession;
        }

        void MakeSureTokenHasntExpiredYet(UserLoginSession userLoginSession)
        {
            DateTime expires = userLoginSession.Expires;
            DateTime now = _timeProvider.Now();
            if (expires < now)
            {
                throw new TokenExpiredException();
            }
        }
    }
}