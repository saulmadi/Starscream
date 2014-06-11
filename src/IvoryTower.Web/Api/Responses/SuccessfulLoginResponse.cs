using System;

namespace IvoryTower.Web.Api.Responses
{
    public class SuccessfulLoginResponse<T>
    {
        public SuccessfulLoginResponse()
        {

        }

        public SuccessfulLoginResponse(T token, string name, DateTime expires)
        {
            Token = token;
            Name = name;
            Expires = expires;
        }

        public T Token { get; private set; }
        public string Name { get; private set; }
        public DateTime Expires { get; private set; }
    }
}