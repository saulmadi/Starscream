namespace Starscream.Domain.Services
{
    public interface ITokenGenerator<out T>
    {
        T Generate();
    }
}