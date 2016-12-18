using System;
using System.Linq;

namespace _16._02
{
    class Program
    {
        static void Main()
        {
            const int diskSize = 35651584;
            var a = "01111010110010011";

            while (a.Length < diskSize)
            {
                var b = new string(new string(a.Reverse().ToArray()).Select(c => c == '0' ? '1' : '0').ToArray());
                a += $"0{b}";
            }

            string data = a.Substring(0, diskSize);

            var checksum = CalculateChecksum(data);

            while (checksum.Length % 2 == 0)
                checksum = CalculateChecksum(checksum);

            Console.WriteLine(checksum);
            Console.ReadKey();
        }

        static string CalculateChecksum(string data)
        {
            var pairs = Enumerable.Range(0, data.Length / 2).Select(i => data.Substring(i * 2, 2)).ToList();

            var checksum = new string(pairs.Select(p => p[0] == p[1] ? '1' : '0').ToArray());

            return checksum;
        }
    }
}