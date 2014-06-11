using IvoryTower.Domain.Entities;
using IvoryTower.Domain.Services;
using IvoryTower.Web.Api.Infrastructure.Authentication;
using Nancy;

namespace IvoryTower.Web.Api.Modules
{
    public static class InvioIdentityExtentions
    {
        public static IUserSession UserSession(this NancyModule module)
        {
            var user = module.Context.CurrentUser as IvoryTowerUserIdentity;
            if (user == null) throw new NoCurrentUserException();
            return user.UserSession;
        }

        public static UserLoginSession UserLoginSession(this NancyModule module)
        {
            var user = module.Context.CurrentUser as IvoryTowerUserIdentity;
            if (user == null || user.UserSession.GetType() != typeof(UserLoginSession)) throw new NoCurrentUserException();
            return (UserLoginSession) user.UserSession;
        }
    }
}