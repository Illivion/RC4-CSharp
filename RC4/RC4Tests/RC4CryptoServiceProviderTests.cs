using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Security.Cryptography;

namespace RC4.Tests
{
    [TestClass()]
    public class RC4CryptoServiceProviderTests
    {
        private readonly Byte[] _key = new Byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };

        private readonly Byte[] _iv = new Byte[0];

        [TestMethod()]
        public void CreateEncryptorTest()
        {
            var provider = GetServiceProvider();

            var encryptor = provider.CreateEncryptor(_key, _iv);

            Assert.IsNotNull(encryptor);
        }

        [TestMethod()]
        public void CreateDecryptorTest()
        {
            var provider = GetServiceProvider();

            var encryptor = provider.CreateDecryptor(_key, _iv);

            Assert.IsNotNull(encryptor);
        }

        [TestMethod()]
        public void GenerateKeyTest()
        {
            var provider = GetServiceProvider();

            provider.GenerateKey();
        }

        [TestMethod()]
        public void GenerateIVTest()
        {
            try
            {
                var provider = GetServiceProvider();

                provider.GenerateIV();
            }
            catch (CryptographicException)
            {
                
            }
        }
        private static RC4CryptoServiceProvider GetServiceProvider()
        {
            var provider = new RC4CryptoServiceProvider();

            return provider;
        }
    }
}