using System;
using System.IO;
using System.Linq;

namespace _03._02
{
    class Program
    {
        static void Main(string[] args)
        {
            var result =
                File.ReadAllLines("input.txt")
                .Select(line => line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select((value, index) => new { index, value = int.Parse(value) } ).ToArray())
                .SelectMany(i => i)
                .GroupBy(a => a.index)
                .Select(p => p.Select( (x, index) => new {x, index}).GroupBy(n => n.index / 3).ToList())
                .SelectMany(q => q, (a,b) => b.ToList().Select(p => p.x).Select(x => x.value).OrderBy(t => t).ToList())
                .Count(r => r[0] + r[1] > r[2]);

            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
