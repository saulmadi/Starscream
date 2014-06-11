using System;
using System.Configuration;

namespace IvoryTower.Domain.Services
{
    public class TokenExpirationProvider : ITokenExpirationProvider
    {
        public DateTime GetExpiration(DateTime now)
        {
            var expirationDays = Convert.ToInt32((string)(ConfigurationManager.AppSettings["PasswordExpirationDays"] ?? "10"));
            return now.AddDays(expirationDays);
        }
    }
}