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

        public T Token { get; set; }
        public string Name { get; set; }
        public DateTime Expires { get; set; }
    }
}