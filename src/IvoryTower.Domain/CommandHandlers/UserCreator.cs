using System;
using IvoryTower.Domain.Commands;
using IvoryTower.Domain.DomainEvents;
using IvoryTower.Domain.Entities;
using IvoryTower.Domain.Services;

namespace IvoryTower.Domain.CommandHandlers
{
    public class UserCreator : ICommandHandler
    {
        readonly IWriteableRepository _writeableRepository;

        public UserCreator(IWriteableRepository writeableRepository)
        {
            _writeableRepository = writeableRepository;
        }

        #region ICommandHandler Members

        public Type CommandType
        {
            get { return typeof (CreateUser); }
        }

        public void Handle(IUserSession userIssuingCommand, object command)
        {
            var c = (CreateUser) command;
            _writeableRepository.Create(new User(c.Name, c.Email, c.EncryptedPassword, c.PhoneNumber));
            NotifyObservers(new UserCreated(c.Email, c.Name, c.PhoneNumber));
        }

        public event DomainEvent NotifyObservers;

        #endregion
    }
}