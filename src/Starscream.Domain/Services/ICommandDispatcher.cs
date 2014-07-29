namespace Starscream.Domain.Services
{
    public interface ICommandDispatcher
    {
        void Dispatch(IUserSession userSession, object command);
    }
}