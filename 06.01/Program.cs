using System;
using System.IO;
using System.Linq;

namespace _06._01
{
    class Program
    {
        static void Main()
        {
            var result =
                File.ReadAllLines("input.txt")
                    .SelectMany(line => line.Select( (@char, index) => new {@char, index}))
                    .GroupBy(g => g.index, (key, group) => group.ToList().Select(c => c.@char).ToList() )
                    .Select(g => g.GroupBy(p => p, (key, group) => 
                    new
                    {
                        key,
                        count = group.Count()
                    }).OrderByDescending(o => o.count).First())
                   .Select(p => p.key.ToString())
                   .Aggregate((a, b) => a.ToString() + b.ToString());

            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
