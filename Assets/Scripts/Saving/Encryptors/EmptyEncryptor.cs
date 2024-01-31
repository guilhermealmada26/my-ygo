namespace BBG.Encryption
{
    public class EmptyEncryptor : IEncryptor
    {
        public string Decrypt(string encrypted)
        {
            return encrypted;
        }

        public string Encrypt(string data)
        {
            return data;
        }
    }
}