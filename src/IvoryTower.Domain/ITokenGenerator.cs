namespace IvoryTower.Domain
{
    public interface ITokenGenerator<out T>
    {
        T Generate();
    }
}