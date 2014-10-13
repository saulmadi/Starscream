using System.Linq;
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
        private readonly IReadOnlyRepository _readOnlyRepository;

        public UserEmailCreator(IWriteableRepository writeableRepository, IReadOnlyRepository readOnlyRepository)
        {
            _writeableRepository = writeableRepository;
            _readOnlyRepository = readOnlyRepository;
        }

        #region ICommandHandler Members

        public void Handle(IUserSession userIssuingCommand, CreateEmailLoginUser command)
        {
            var userCreated = new UserEmailLogin(command.Name, command.Email, command.EncryptedPassword,
                command.PhoneNumber);

            var listOfGuidAbilities = command.abilities;
            var listOfAbilities = _readOnlyRepository.Query<UserAbility>(x => listOfGuidAbilities.Any(z => x.Id == z));

            listOfAbilities.ToList().ForEach(userCreated.AddAbility);

            _writeableRepository.Create(userCreated);
                //_writeableRepository.Create(new UserEmailLogin(command.Name, command.Email, command.EncryptedPassword, command.PhoneNumber));


            NotifyObservers(new UserEmailCreated(userCreated.Id,command.Email, command.Name, command.PhoneNumber));
        }

        public event DomainEvent NotifyObservers;

        #endregion
    }
}