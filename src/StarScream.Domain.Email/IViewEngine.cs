namespace StarScream.Domain.Email
{
    public interface IViewEngine
    {
        string Render(object model, string formattedString);
    }
}