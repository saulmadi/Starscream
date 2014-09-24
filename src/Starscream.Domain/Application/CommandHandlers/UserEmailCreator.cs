using AcklenAvenue.Commands;
using Starscream.Domain.Application.Commands;
using Starscream.Domain.DomainEvents;
using Starscream.Domain.Entities;
using Starscream.Domain.Services;


namespace Starscream.Domain.Application.CommandHandlers
{
    public class UserEmailCreator : ICommandHandler<CreateEmailLoginUser>
    {
        readonly IWriteableRepository _writeableRepository;

        public UserEmailCreator(IWriteableRepository writeableRepository)
        {
            _writeableRepository = writeableRepository;
        }

        #region ICommandHandler Members

        public void Handle(IUserSession userIssuingCommand, CreateEmailLoginUser command)
        {
            var userCreated = _writeableRepository.Create(new UserEmailLogin(command.Name, command.Email, command.EncryptedPassword, command.PhoneNumber));
            NotifyObservers(new UserEmailCreated(userCreated.Id,command.Email, command.Name, command.PhoneNumber));
        }

        public event DomainEvent NotifyObservers;

        #endregion
    }
}