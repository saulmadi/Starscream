namespace Starscream.Notifications
{
    public interface ICommandDispatcher
    {
        void Dispatch(IUserSession userSession, object command);
    }
}