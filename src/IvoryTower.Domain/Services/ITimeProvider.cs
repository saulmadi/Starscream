using System;

namespace IvoryTower.Domain.Services
{
    public interface ITimeProvider
    {
        DateTime Now();
    }
}