using System;

namespace IvoryTower.Domain
{
    public interface ITokenExpirationProvider
    {
        DateTime GetExpiration(DateTime now);
    }
}