using System.Security.Cryptography;

namespace Services.HashGenerator
{
    public static class Hashgenerator
    {
        public static string GetHash(byte[] file)
        {
            if (file != null)
            {
                SHA1 sha1 = SHA1.Create();
                byte[] hashBytes = sha1.ComputeHash(file);
                string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);

                return hash;
            }
            throw new ArgumentNullException();
        }
    }
}
