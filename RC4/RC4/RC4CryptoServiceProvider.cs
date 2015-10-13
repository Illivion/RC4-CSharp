using System;
using System.Security.Cryptography;

namespace RC4
{
    public sealed class RC4CryptoServiceProvider : SymmetricAlgorithm
    {
        private Byte[] _key;

        public const Int32 DefaultKeyLength = 8;

        public override ICryptoTransform CreateEncryptor(Byte[] rgbKey, Byte[] rgbIV)
        {
            _key = rgbKey;

            return new RC4CryptoTransform(_key);
        }

        public override ICryptoTransform CreateDecryptor(Byte[] rgbKey, Byte[] rgbIV)
        {
            return CreateEncryptor(rgbKey, rgbIV);
        }

        public override void GenerateKey()
        {
            var rnd = new RNGCryptoServiceProvider();

            _key = new Byte[DefaultKeyLength];

            rnd.GetBytes(_key);
        }

        public override void GenerateIV()
        {
            throw new CryptographicException("RC4 cipher do not support IV generation");
        }
    }
}