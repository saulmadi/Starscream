using System;
using System.Collections.Generic;
using System.Linq;
using Starscream.Domain.Application.Commands;
using Starscream.Domain.Entities;
using Starscream.Domain.Exceptions;
using Starscream.Domain.Services;

namespace Starscream.Domain.Validators
{
    public class PasswordResetValidator : ICommandValidator<CreatePasswordResetToken>
    {
        readonly IReadOnlyRepository _readOnlyRepsitory;

        public PasswordResetValidator(IReadOnlyRepository readOnlyRepsitory)
        {
            _readOnlyRepsitory = readOnlyRepsitory;
        }

        public void Validate(IUserSession userSession, CreatePasswordResetToken command)
        {
            var validationFailures = new List<ValidationFailure>();

            if (string.IsNullOrEmpty(command.Email))
                validationFailures.Add(new ValidationFailure("Email", ValidationFailureType.Missing));
            else
            {
                try
                {
                    _readOnlyRepsitory.First<User>(x => x.Email == command.Email);
                }
                catch (ItemNotFoundException<User>)
                {
                    validationFailures.Add(new ValidationFailure("Email", ValidationFailureType.DoesNotExist));
                }
            }

            if (validationFailures.Any())
                throw new CommandValidationException(validationFailures);
        }
    }
}