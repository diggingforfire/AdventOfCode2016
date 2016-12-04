using System;
using System.IO;
using System.Linq;

namespace _04._01
{
    class Program
    {
        static void Main()
        {
            var result =
                File.ReadAllLines("input.txt")
                    .Select(line => line.Split('-'))
                    .Select(parts => new
                    {
                        Name = parts.Where(l => !l.Contains('[')).ToList().Aggregate((i, j) => i + j),
                        SectorId = int.Parse(parts.Last().Substring(0, parts.Last().IndexOf("["))),
                        Checksum = parts.Last().Substring(parts.Last().IndexOf("[") + 1, parts.Last().IndexOf("]") - parts.Last().IndexOf("[") - 1)
                    })
                    .Where(a => a.Checksum == new string(a.Name.GroupBy(c => c).OrderByDescending(g => g.Count()).ThenBy(g => (int)g.Key).Take(5).Select(p => p.Key).ToArray()))
                    .Sum(a => a.SectorId);

            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
