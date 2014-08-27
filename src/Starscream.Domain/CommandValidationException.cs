using System;
using System.Collections.Generic;

namespace Starscream.Domain
{
    public class CommandValidationException : Exception
    {
        public CommandValidationException(IEnumerable<ValidationFailure> failures)
            : base("The command was issued with invalid properties.")
        {
            ValidationFailures = failures;
        }

        public IEnumerable<ValidationFailure> ValidationFailures { get; private set; }
    }
}