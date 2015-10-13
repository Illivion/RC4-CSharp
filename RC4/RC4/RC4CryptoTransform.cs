using System;
using System.Security.Cryptography;

namespace RC4
{
    public sealed class RC4CryptoTransform : ICryptoTransform
    {
        private Byte[] _rgbKey;

        // Bits, encrypted for one iteration
        public const Int32 BlockSizeInBits = 8;

        public const Int32 BlockSizeInBytes = BlockSizeInBits/8;

        public const Int32 SBlockSize = BlockSizeInBits*BlockSizeInBits;

        private Byte[] _sBlock;

        private Int32 _rndI = 0;
        private Int32 _rndJ = 0;

        public RC4CryptoTransform(byte[] rgbKey)
        {
            _rgbKey = rgbKey;

            _sBlock = new Byte[SBlockSize];

            Initialize();
        }

        private void Initialize()
        {
            var blockSize = SBlockSize;

            int keyLength = _rgbKey.Length;

            for (int i = 0; i < blockSize; i++)
            {
                _sBlock[i] = (byte)i;
            }

            int j = 0;

            for (int i = 0; i < blockSize; i++)
            {
                j = (j + _sBlock[i] + _rgbKey[i % keyLength]) % blockSize;

                _sBlock.Swap(i, j);
            }
        }

        private Byte GetNextPseudoRandomItem()
        {
            var blockSize = SBlockSize;

            _rndI = (_rndI + 1) % blockSize;
            _rndJ = (_rndJ + _sBlock[_rndI]) % blockSize;

            _sBlock.Swap(_rndI, _rndJ);

            return _sBlock[(_sBlock[_rndI] + _sBlock[_rndJ]) % blockSize];
        }

        public void Dispose()
        {
            Array.Clear(_rgbKey,0,_rgbKey.Length);
            Array.Clear(_sBlock, 0, _sBlock.Length);

            _rgbKey = null;
            _sBlock = null;
        }

        public Int32 TransformBlock(Byte[] inputBuffer, Int32 inputOffset, Int32 inputCount, Byte[] outputBuffer, Int32 outputOffset)
        {
            for (long i = inputOffset; i < inputOffset + inputCount; i++)
            {
                outputBuffer[outputOffset + i] = (byte) (inputBuffer[i] ^ GetNextPseudoRandomItem());
            }

            return inputCount;
        }

        public Byte[] TransformFinalBlock(Byte[] inputBuffer, Int32 inputOffset, Int32 inputCount)
        {
            var encryptedData = new Byte[inputCount];

            TransformBlock(inputBuffer, inputOffset, inputCount, encryptedData, 0);
            
            return encryptedData;
        }

        public Int32 InputBlockSize => BlockSizeInBytes;

        public Int32 OutputBlockSize => BlockSizeInBytes;

        public Boolean CanTransformMultipleBlocks => false;

        public Boolean CanReuseTransform => false;
    }
}