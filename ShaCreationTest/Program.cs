
using System.Diagnostics;
using System.Numerics;
using System.Security.Cryptography;

namespace ShaCreationTest
{
    class Program
    {

        static void Main()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            var i = 0;
            double time = 0;
            while (i < 10)
            {
                var timer = Stopwatch.StartNew();
                Hash();
                timer.Stop();
                time += timer.Elapsed.TotalMilliseconds;
                i += 1;
            }
            Console.WriteLine($"Avg Default Timer: {time / 10:F3} ms");
            i = 0;
            time = 0;
            while (i < 10)
            {
                var timer = Stopwatch.StartNew();
                HashLoop(10000);
                timer.Stop();
                time += timer.Elapsed.TotalMilliseconds;
                i += 1;
            }
            Console.WriteLine($"Avg Loop Timer: {time / 10:F3} ms");
            i = 0;
            time = 0;
            while (i < 10)
            {
                var timer = Stopwatch.StartNew();
                HashParallel();
                timer.Stop();
                time += timer.Elapsed.TotalMilliseconds;
                i += 1;
            }
            Console.WriteLine($"Avg Loop Timer: {time / 10:F3} ms");
        }

        public static void Hash()
        {
            using (SHA256 sHA256 = SHA256.Create())
            {
                BigInteger max = new BigInteger(1) << 128;
                RandomBigInteger random = new RandomBigInteger();
                var value = random.NextBigInteger(0, max);
                var data = sHA256.ComputeHash(value.ToByteArray());
            }
        }

        public static void HashLoop(int limit)
        {
            var i = 0;
            while (i < limit)
            {
                using (SHA256 sHA256 = SHA256.Create())
                {
                    BigInteger max = new BigInteger(1) << 128;
                    RandomBigInteger random = new RandomBigInteger();
                    var value = random.NextBigInteger(0, max);
                    var data = sHA256.ComputeHash(value.ToByteArray());
                }
                i += 1;
            }
        }

        public static void HashParallel()
        {
            Parallel.For(0, 100000, val =>
            {
                using (SHA256 sHA256 = SHA256.Create())
                {
                    BigInteger max = new BigInteger(1) << 128;
                    RandomBigInteger random = new RandomBigInteger();
                    var value = random.NextBigInteger(0, max);
                    var data = sHA256.ComputeHash(value.ToByteArray());
                }
            });
        }
    }
}