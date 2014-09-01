using System;

namespace AcklenAvenue.Commands
{
    public interface IUserSession
    {
        Guid Id { get; }
    }
}