namespace Starscream.Notifications
{
    public interface ICommandHandler
    {
        event DomainEvent NotifyObservers;
    }

    public interface ICommandHandler<in T> : ICommandHandler
    {
        void Handle(IUserSession userIssuingCommand, T command);
    }
}