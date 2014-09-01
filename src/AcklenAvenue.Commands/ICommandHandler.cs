namespace AcklenAvenue.Commands
{
    public interface ICommandHandler<in T> : ICommandHandler
    {
        void Handle(IUserSession userIssuingCommand, T command);        
    }

    public interface ICommandHandler
    {
        event DomainEvent NotifyObservers;
    }
}