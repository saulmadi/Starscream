using IvoryTower.Domain.ValueObjects;

namespace IvoryTower.Domain.Commands
{
    public class CreateUser
    {
        public string Email { get; private set; }
        public EncryptedPassword EncryptedPassword { get; private set; }
        public string Name { get; private set; }
        public string PhoneNumber { get; private set; }

        public CreateUser(string email, EncryptedPassword password, string name, string phoneNumber)
        {
            Email = email;
            EncryptedPassword = password;
            Name = name;
            PhoneNumber = phoneNumber;
        }
    }
}