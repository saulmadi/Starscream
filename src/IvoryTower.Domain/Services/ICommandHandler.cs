using System;

namespace IvoryTower.Domain.Services
{
    public interface ICommandHandler
    {
        Type CommandType { get; }
        void Handle(IUserSession userIssuingCommand, object command);
        event DomainEvent NotifyObservers;
    }
}