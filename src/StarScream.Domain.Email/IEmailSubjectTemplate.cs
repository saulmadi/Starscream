using System;

namespace StarScream.Domain.Email
{
    public interface IEmailSubjectTemplate
    {
        Type ForType { get; }
        string SubjectTemplate { get; }
    }
}