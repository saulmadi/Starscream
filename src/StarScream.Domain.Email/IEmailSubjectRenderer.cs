namespace StarScream.Domain.Email
{
    public interface IEmailSubjectRenderer
    {
        string Render<T>(T model);
    }
}