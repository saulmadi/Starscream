namespace Starscream.Domain.DomainEvents
{
    public class UserFacebookCreated
    {
        public string email { get; protected set; }
        public string name { get; protected set; }
        public string id { get; protected set; }

        public UserFacebookCreated(string email, string name, string id)
        {
            this.email = email;
            this.name = name;
            this.id = id;
        }
    }
}