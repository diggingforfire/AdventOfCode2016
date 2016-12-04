using System;
using System.IO;
using System.Linq;

namespace _04._02
{
    static class Program
    {
        static void Main(string[] args)
        {
            var result =
            File.ReadAllLines("input.txt")
                .Select(line => line.Split('-'))
                .Select(parts => new
                {
                    Name = parts.Where(l => !l.Contains('[')).ToList().Aggregate((i, j) => i + " " + j),
                    SectorId = int.Parse(parts.Last().Substring(0, parts.Last().IndexOf("["))),
                    Checksum = parts.Last().Substring(parts.Last().IndexOf("[") + 1, parts.Last().IndexOf("]") - parts.Last().IndexOf("[") - 1)
                })
                .Where(a => a.Checksum == new string(a.Name.Where(c => c != ' ').GroupBy(c => c).OrderByDescending(g => g.Count()).ThenBy(g => (int)g.Key).Take(5).Select(p => p.Key).ToArray()))
                .Select(p => 
                  new { p.SectorId, DecryptedName = new string(p.Name.Select(c => c == ' ' ? ' ' : (char)( (c - 'a' + p.SectorId) % 26 + 'a') ).ToArray()) }
                )
                .Single(p => p.DecryptedName.ToLowerInvariant() == "northpole object storage").SectorId;
                
            Console.WriteLine(result);
            Console.ReadKey();
        }

    }
}
