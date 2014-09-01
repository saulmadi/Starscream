namespace AcklenAvenue.Commands
{
    public interface ICommandDispatcher
    {
        void Dispatch(IUserSession userSession, object command);
    }
}