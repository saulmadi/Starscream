using System;

namespace IvoryTower.Domain.Services
{
    public interface ITokenExpirationProvider
    {
        DateTime GetExpiration(DateTime now);
    }
}