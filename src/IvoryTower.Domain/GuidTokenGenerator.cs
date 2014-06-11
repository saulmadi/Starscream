using System;

namespace IvoryTower.Domain
{
    public class GuidTokenGenerator : ITokenGenerator<Guid>
    {
        public Guid Generate()
        {
            return Guid.NewGuid();
        }
    }
}