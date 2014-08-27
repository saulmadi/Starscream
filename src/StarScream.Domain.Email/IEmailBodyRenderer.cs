namespace StarScream.Domain.Email
{
    public interface IEmailBodyRenderer
    {
        string Render<T>(T model);
    }
}