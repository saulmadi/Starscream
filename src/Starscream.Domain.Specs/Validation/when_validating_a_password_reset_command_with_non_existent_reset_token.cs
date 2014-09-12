using System;
using System.Collections.Generic;
using Machine.Specifications;
using Moq;
using Starscream.Domain.Application.Commands;
using Starscream.Domain.Entities;
using Starscream.Domain.Exceptions;
using Starscream.Domain.Services;
using Starscream.Domain.Validators;
using Starscream.Domain.ValueObjects;
using It = Machine.Specifications.It;

namespace Starscream.Domain.Specs.Validation
{
    public class when_validating_a_password_reset_command_with_non_existent_reset_token
    {
        static ICommandValidator<ResetPassword> _validator;
        static readonly EncryptedPassword EncryptedPassword = new EncryptedPassword("password");
        static readonly Guid ResetPasswordToken = Guid.NewGuid();
        static List<ValidationFailure> _expectedFailures;
        static Exception _exception;
        static IReadOnlyRepository _readOnlyRepo;

        Establish context =
            () =>
            {
                _readOnlyRepo = Mock.Of<IReadOnlyRepository>();
                _validator = new PassowrdResetValidator(_readOnlyRepo);

                _expectedFailures = new List<ValidationFailure>
                                    {
                                        new ValidationFailure(
                                            "ResetPasswordToken",
                                            ValidationFailureType.DoesNotExist)
                                    };

                Mock.Get(_readOnlyRepo).Setup(x => x.GetById<PasswordResetToken>(ResetPasswordToken))
                    .Throws(new ItemNotFoundException<PasswordResetToken>(ResetPasswordToken));
            };

        Because of =
            () => _exception = Catch.Exception(() =>
                _validator.Validate(new VisitorSession(),
                    new ResetPassword(ResetPasswordToken, EncryptedPassword)));

        It should_return_expected_failures =
            () => ((CommandValidationException) _exception).ValidationFailures.ShouldBeLike(_expectedFailures);
    }
}