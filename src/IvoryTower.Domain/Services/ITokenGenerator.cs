namespace IvoryTower.Domain.Services
{
    public interface ITokenGenerator<out T>
    {
        T Generate();
    }
}