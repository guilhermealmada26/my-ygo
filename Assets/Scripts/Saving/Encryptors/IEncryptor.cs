namespace BBG.Encryption
{
    public interface IEncryptor
    {
        string Encrypt(string data);
        string Decrypt(string encrypted);
    }
}