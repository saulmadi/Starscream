using System;
using Starscream.Domain.Entities;

namespace Starscream.Domain.Specs.Stubs
{
    public class TestUser : User
    {
        public TestUser(Guid userId, string name, string password)
        {
            Id = userId;
            Name = name;
            EncryptedPassword = password;
        }
    }
}