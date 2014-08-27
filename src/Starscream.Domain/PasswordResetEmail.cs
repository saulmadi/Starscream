using System;

namespace Starscream.Domain
{
    public class PasswordResetEmail
    {
        public Guid Token { get; private set; }

        public PasswordResetEmail(Guid token)
        {
            Token = token;
        }
    }
}