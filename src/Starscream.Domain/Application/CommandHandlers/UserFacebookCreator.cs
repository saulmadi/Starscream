using System;
using Starscream.Domain.Application.Commands;
using Starscream.Domain.Services;

namespace Starscream.Domain.Application.CommandHandlers
{
    public class UserFacebookCreator : ICommandHandler<CreateFacebookLoginUser>
    {
        readonly IWriteableRepository _writeableRepository;

        public UserFacebookCreator(IWriteableRepository writeableRepository)
        {
            _writeableRepository = writeableRepository;
        }

        public void Handle(IUserSession userIssuingCommand, CreateFacebookLoginUser command)
        {
            throw new NotImplementedException();
        }

        public event DomainEvent NotifyObservers;
    }
}