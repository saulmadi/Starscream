using System;

namespace IvoryTower.Domain
{
    public interface ITimeProvider
    {
        DateTime Now();
    }
}