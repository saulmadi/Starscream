using System;
using NHibernate.Util;

namespace Starscream.Web.Api.Responses
{
    public class SuccessfulLoginResponse<T>
    {
        public SuccessfulLoginResponse()
        {

        }

        public SuccessfulLoginResponse(T token, string name, DateTime expires, string claims)
        {
            Token = token;
            Name = name;
            Expires = expires;
            Claims = claims.Split(',');
        }

        public T Token { get; set; }
        public string Name { get; set; }
        public DateTime Expires { get; set; }
        public string[] Claims { get; set; }
    }
}