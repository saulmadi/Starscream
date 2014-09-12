namespace Starscream.Domain.Entities
{
    public class UserFacebookLogging:User
    {
        public virtual string NickName { get; protected set; }
        public virtual string FirstName { get; protected set; }
        public virtual string  LastName { get; protected set; }
        public virtual string URL { get; protected set; }
        public virtual string ImageUrl { get; protected set; }

        protected UserFacebookLogging()
        {
            
        }



    }
}