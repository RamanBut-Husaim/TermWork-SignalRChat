namespace BSUIR.TermWork.ImageViewer.Shared
{
    using System;

    internal class MersenneTwister
    {
        // period parameters

        #region Constants

        private const uint LowerMask = 0x7fffffff; // least significant r bits
        private const int M = 397;
        private const uint MatrixA = 0x9908b0df; // constant vector a
        private const int N = 624;
        // tempering parameters
        private const uint TemperingMaskB = 0x9d2c5680;
        private const uint TemperingMaskC = 0xefc60000;
        private const uint UpperMask = 0x80000000; // most significant w-r bits

        #endregion

        #region Static Fields

        private static readonly uint[] mag01 = { 0x0, MatrixA };

        #endregion

        //the array for the state vector

        #region Fields

        private readonly uint[] _mt = new uint[N];

        private short _mti;

        #endregion

        // initializing the array with a NONZERO seed

        #region Constructors and Destructors

        public MersenneTwister(uint seed)
        {
            this._mt[0] = seed & 0xffffffffU;
            for (this._mti = 1; this._mti < N; ++this._mti)
            {
                this._mt[this._mti] = (69069 * this._mt[this._mti - 1]) & 0xffffffffU;
            }
        }

        //a default initial seed is used
        public MersenneTwister()
            : this(4357)
        {
        }

        #endregion

        #region Public Methods and Operators

        public int Next()
        {
            return this.Next(int.MaxValue);
        }

        public int Next(int maxValue)
        {
            if (maxValue <= 1)
            {
                if (maxValue < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }

                return 0;
            }

            return (int)(this.NextDouble() * maxValue);
        }

        public int Next(int minValue, int maxValue)
        {
            if (maxValue < minValue)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (maxValue == minValue)
            {
                return minValue;
            }
            return this.Next(maxValue - minValue) + minValue;
        }

        public void NextBytes(byte[] buffer)
        {
            int bufLen = buffer.Length;

            if (buffer == null)
            {
                throw new ArgumentNullException();
            }

            for (int idx = 0; idx < bufLen; ++idx)
            {
                buffer[idx] = (byte)this.Next(256);
            }
        }

        public double NextDouble()
        {
            return (double)this.GenerateUInt() / ((ulong)uint.MaxValue + 1);
        }

        public virtual uint NextUInt()
        {
            return this.GenerateUInt();
        }

        public virtual uint NextUInt(uint maxValue)
        {
            return (uint)(this.GenerateUInt() / ((double)uint.MaxValue / maxValue));
        }

        public virtual uint NextUInt(uint minValue, uint maxValue)
        {
            if (minValue >= maxValue)
            {
                throw new ArgumentOutOfRangeException();
            }

            return (uint)(this.GenerateUInt() / ((double)uint.MaxValue / (maxValue - minValue)) + minValue);
        }

        #endregion

        #region Methods

        protected uint GenerateUInt()
        {
            uint y;

            // mag01[x] = x * MatrixA  for x=0,1
            if (this._mti >= N) // generate N words at one time
            {
                short kk = 0;

                for (; kk < N - M; ++kk)
                {
                    y = (this._mt[kk] & UpperMask) | (this._mt[kk + 1] & LowerMask);
                    this._mt[kk] = this._mt[kk + M] ^ (y >> 1) ^ mag01[y & 0x1];
                }

                for (; kk < N - 1; ++kk)
                {
                    y = (this._mt[kk] & UpperMask) | (this._mt[kk + 1] & LowerMask);
                    this._mt[kk] = this._mt[kk + (M - N)] ^ (y >> 1) ^ mag01[y & 0x1];
                }

                y = (this._mt[N - 1] & UpperMask) | (this._mt[0] & LowerMask);
                this._mt[N - 1] = this._mt[M - 1] ^ (y >> 1) ^ mag01[y & 0x1];

                this._mti = 0;
            }

            y = this._mt[this._mti++];
            y ^= TemperingShiftU(y);
            y ^= TemperingShiftS(y) & TemperingMaskB;
            y ^= TemperingShiftT(y) & TemperingMaskC;
            y ^= TemperingShiftL(y);

            return y;
        }

        private static uint TemperingShiftL(uint y)
        {
            return (y >> 18);
        }

        private static uint TemperingShiftS(uint y)
        {
            return (y << 7);
        }

        private static uint TemperingShiftT(uint y)
        {
            return (y << 15);
        }

        private static uint TemperingShiftU(uint y)
        {
            return (y >> 11);
        }

        #endregion
    }
}