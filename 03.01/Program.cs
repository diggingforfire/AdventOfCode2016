using System;
using System.IO;
using System.Linq;

namespace _03._01
{
    class Program
    {
        static void Main(string[] args)
        {
            var result =
                File.ReadAllLines("input.txt")
                .Select(i => i.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray().OrderBy(t => t).ToList())
                .Count(r => r[0] + r[1] > r[2]);

            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
