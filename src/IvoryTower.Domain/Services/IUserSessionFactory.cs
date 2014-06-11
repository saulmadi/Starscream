using IvoryTower.Domain.Entities;

namespace IvoryTower.Domain.Services
{
    public interface IUserSessionFactory
    {
        UserLoginSession Create(User executor);
    }
}