namespace StarScream.Domain.Email
{
    public interface IEmailTemplateProvider
    {
        string GetTemplateFor<T>(T model);
    }
}