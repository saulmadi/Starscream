namespace IvoryTower.Domain
{
    public interface IPasswordEncryptor
    {
        EncryptedPassword Encrypt(string clearTextPassword);
    }
}