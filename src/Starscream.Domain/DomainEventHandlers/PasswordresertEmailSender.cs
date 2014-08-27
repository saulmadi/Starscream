using BlingBag;
using Starscream.Domain.DomainEvents;
using Starscream.Domain.Entities;
using Starscream.Domain.Services;

namespace Starscream.Domain.DomainEventHandlers
{
    public class PasswordResertEmailSender : IBlingHandler<PasswordResetTokenCreated>
    {
        readonly IReadOnlyRepository _readOnlyRepository;
        readonly IEmailSender _emailSender;

        public PasswordResertEmailSender(IReadOnlyRepository readOnlyRepository, IEmailSender emailSender)
        {
            _readOnlyRepository = readOnlyRepository;
            _emailSender = emailSender;
        }

        public void Handle(PasswordResetTokenCreated @event)
        {
            var user = _readOnlyRepository.GetById<User>(@event.UserId);
            _emailSender.Send(user.Email, new PasswordResetEmail(@event.TokenId));
        }
    }
}