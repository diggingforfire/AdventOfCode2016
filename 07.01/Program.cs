using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _07._01
{
    static class Program
    {
        static void Main()
        {
            var result =
                File.ReadAllLines("input.txt")
                .Select(line => new
                {
                    line,
                    hypernets = new Regex(@"\[(.*?)\]").Matches(line).Cast<Match>().Select(m => m.Value.Trim('[', ']')).ToList()
                })
                .Select(h =>
                {
                    string line = h.line;
                    h.hypernets.ForEach(hh => line = line.Replace($"[{hh}]", "-")); // regex schmegex
                    return new { h.hypernets, supernets = line.Split('-').ToList() };
                })
                .Count(h => !h.hypernets.Any(hh => hh.HasAbba()) && h.supernets.Any(hh => hh.HasAbba()));
                   
            Console.WriteLine(result);
            Console.ReadKey();
        }

        static bool HasAbba(this string ip)
        {
            return ip.Skip(1).Select((c, i) => i + 3 > ip.Length - 1  ? null :
            new
            {
                first = ip[i].ToString() + ip[i + 1].ToString(),
                second = ip[i + 2].ToString() + ip[i + 3].ToString()
            }).Any(p => p != null && p.first != p.second && p.first == new string(p.second.Reverse().ToArray()));
        }
    }
}
