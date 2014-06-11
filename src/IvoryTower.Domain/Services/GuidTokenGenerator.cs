using System;

namespace IvoryTower.Domain.Services
{
    public class GuidTokenGenerator : ITokenGenerator<Guid>
    {
        public Guid Generate()
        {
            return Guid.NewGuid();
        }
    }
}