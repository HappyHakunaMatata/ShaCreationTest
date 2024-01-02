using System;
using System.Numerics;

namespace ShaCreationTest
{
	public class RandomBigInteger:Random
	{
		public RandomBigInteger():base()
		{
		}

        public RandomBigInteger(int Seed) : base(Seed)
        {

        }

		public BigInteger NextBigInteger(int bitLenght)
		{
			if (bitLenght < 1)
			{
				return BigInteger.Zero;
			}
			int bytes = bitLenght / 8;
			int bits = bitLenght % 8;
			byte[] bs = new byte[bytes + 1];
			NextBytes(bs);
			byte mask = (byte)(0xFF >> (8 - bits));
			bs[bs.Length - 1] &= mask;
			return new BigInteger(bs);
		}

		public BigInteger NextBigInteger(BigInteger start, BigInteger end)
		{
			if (start == end)
			{
				return start;

			}
			BigInteger res = end;
			if (start > end)
			{
				end = start;
				start = res;
				res = end - start;

			}
			else
			{
				res -= start;
			}

			byte[] bs = res.ToByteArray();
			int bits = 8;
			byte mask = 0x7F;
			while ((bs[bs.Length-1] & mask) == bs[bs.Length - 1])
			{
				bits--;
				mask >>= 1;

			}
			bits += 8 * bs.Length;
			return ((NextBigInteger(bits + 1) * res) / BigInteger.Pow(2, bits + 1)) + start;
		}


    }
}

