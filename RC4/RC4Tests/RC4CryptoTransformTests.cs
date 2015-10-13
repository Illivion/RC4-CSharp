using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using RC4Tests;

namespace RC4.Tests
{
    [TestClass()]
    public class RC4CryptoTransformTests
    {
        private readonly Byte[] _key = new Byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };

        [TestMethod()]
        public void RC4CryptoTransformTest()
        {
            var transform = GetRC4CryptoTransform();
        }

        [TestMethod()]
        public void TransformBlockTest()
        {
            var testVector = Utils.GetTestVector(40);

            var transform = new RC4CryptoTransform(testVector.Key);

            var inputBuffer = testVector.Data;
            var outputBuffer = new Byte[inputBuffer.Length];

            var encrypted = transform.TransformBlock(inputBuffer, 0, inputBuffer.Length, outputBuffer, 0);

            Assert.AreEqual(encrypted, inputBuffer.Length);
        }

        [TestMethod()]
        public void TransformFinalBlockTest()
        {
            var testVector = Utils.GetTestVector(40);

            var transform = new RC4CryptoTransform(testVector.Key);

            var inputBuffer = testVector.Data;

            var encryptedData = transform.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);

            Assert.IsNotNull(encryptedData);
            Assert.AreEqual(encryptedData.Length, inputBuffer.Length);
        }

        [TestMethod()]
        public void EncryptionDecriptionTest()
        {
            var testVector = Utils.GetTestVector(40);

            var transform = new RC4CryptoTransform(testVector.Key);

            var inputBuffer = testVector.Data;
            var outputBuffer = new Byte[inputBuffer.Length];

            var encrypted = transform.TransformBlock(inputBuffer, 0, inputBuffer.Length, outputBuffer, 0);

            Assert.AreEqual(encrypted, inputBuffer.Length);

            var decryptedData = new Byte[inputBuffer.Length];

            var transform2 = new RC4CryptoTransform(testVector.Key);

            transform2.TransformBlock(outputBuffer, 0, outputBuffer.Length, decryptedData, 0);

            Assert.IsTrue(decryptedData.SequenceEqual(inputBuffer));
        }

        private RC4CryptoTransform GetRC4CryptoTransform()
        {
            return new RC4CryptoTransform(_key);
        }
    }
}