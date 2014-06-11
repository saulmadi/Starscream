using System;

namespace IvoryTower.Domain
{
    public class SystemTimeProvider : ITimeProvider
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}