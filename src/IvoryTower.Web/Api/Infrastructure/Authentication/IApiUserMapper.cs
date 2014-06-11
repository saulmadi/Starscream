using Nancy.Security;

namespace IvoryTower.Web.Api.Infrastructure.Authentication
{
    public interface IApiUserMapper<in T>
    {
        IUserIdentity GetUserFromAccessToken(T token);
    }
}