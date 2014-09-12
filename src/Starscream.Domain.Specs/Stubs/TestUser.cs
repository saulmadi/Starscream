using Starscream.Domain.Entities;

namespace Starscream.Domain.Specs.Stubs
{
    public class TestUser : User
    {
        public TestUser(string name, string password)
        {
            Name = name;
            EncryptedPassword = password;
        }
    }
}