using Starscream.Notifications;

namespace Starscream.Domain.Services
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