using IvoryTower.Domain.ValueObjects;

namespace IvoryTower.Domain.Services
{
    public interface IPasswordEncryptor
    {
        EncryptedPassword Encrypt(string clearTextPassword);
    }
}