namespace BBG.Encryption
{
    public enum EncryptionType
    {
        None, ZIP, AES
    }

    public static class EncryptionTypeExtension
    {
        public static IEncryptor GetEncryptor(this EncryptionType type)
        {
            switch (type)
            {
                case EncryptionType.ZIP:
                    return new ZipCompressEncryptor();
                case EncryptionType.AES:
                    return new AESEncryptor();
            }

            return new EmptyEncryptor();
        }
    }
}