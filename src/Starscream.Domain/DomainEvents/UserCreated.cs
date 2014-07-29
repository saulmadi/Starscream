namespace Starscream.Domain.DomainEvents
{
    public class UserCreated
    {
        public string Email { get; private set; }
        public string Name { get; private set; }
        public string PhoneNumber { get; private set; }

        public UserCreated(string email, string name, string phoneNumber)
        {
            Email = email;
            Name = name;
            PhoneNumber = phoneNumber;
        }
    }
}