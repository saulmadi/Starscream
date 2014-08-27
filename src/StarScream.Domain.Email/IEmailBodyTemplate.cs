using System;

namespace StarScream.Domain.Email
{
    public interface IEmailBodyTemplate
    {
        Type ForType { get; }
        string BodyTemplate { get; }
    }
}