using Starscream.Domain.ValueObjects;

namespace Starscream.Domain.Application.Commands
{
    public class CreateEmailLoginUser
    {
        public string Email { get; private set; }
        public EncryptedPassword EncryptedPassword { get; private set; }
        public string Name { get; private set; }
        public string PhoneNumber { get; private set; }

        public CreateEmailLoginUser(string email, EncryptedPassword password, string name, string phoneNumber)
        {
            Email = email;
            EncryptedPassword = password;
            Name = name;
            PhoneNumber = phoneNumber;
        }
    }



}