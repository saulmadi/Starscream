using System;
using System.Collections.Generic;
using System.Linq;
using Starscream.Domain.Application.Commands;
using Starscream.Domain.Entities;
using Starscream.Domain.Exceptions;
using Starscream.Domain.Services;

namespace Starscream.Domain.Validators
{
    public class PassowrdResetValidator : ICommandValidator<ResetPassword>
    {
        readonly IReadOnlyRepository _readOnlyRepo;

        public PassowrdResetValidator(IReadOnlyRepository readOnlyRepo)
        {
            _readOnlyRepo = readOnlyRepo;
        }

        public void Validate(IUserSession userSession, ResetPassword command)
        {
            var failures = new List<ValidationFailure>();
            if (command.EncryptedPassword==null || string.IsNullOrEmpty(command.EncryptedPassword.Password))
            {
                failures.Add(new ValidationFailure("EncryptedPassword", ValidationFailureType.Missing));
            }
            if (command.ResetPasswordToken == Guid.Empty)
            {
                failures.Add(new ValidationFailure("ResetPasswordToken", ValidationFailureType.Missing));
            }
            else
            {
                try
                {
                    _readOnlyRepo.GetById<PasswordResetToken>(command.ResetPasswordToken);
                }
                catch (ItemNotFoundException<PasswordResetToken>)
                {
                    failures.Add(new ValidationFailure("ResetPasswordToken", ValidationFailureType.DoesNotExist));
                }
            }
            if (failures.Any())
                throw new CommandValidationException(failures);
        }
    }
}