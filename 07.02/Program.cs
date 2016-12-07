using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _07._02
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
                    return new {h.hypernets, supernets = line.Split('-').ToList()};
                })
                .Count(h =>
                {
                    var abas = h.supernets.SelectMany(hh => hh.GetAbas()).Where(hh => hh.Any()).Distinct().ToArray();
                    var babs = h.hypernets.SelectMany(hh => hh.GetBabs(abas)).ToArray();

                    return abas.Any() && babs.Any();
                });

            Console.WriteLine(result);
            Console.ReadKey();
        }

        static string[] GetAbas(this string ip)
        {
            var res = ip.Skip(2).Select((c, i) =>
                    new
                    {
                        seq = new string(new[] {ip[i], ip[++i], c})
                    })
                .Where(s => s.seq[0] == s.seq[2] && s.seq[0] != s.seq[1])
                .Select(s => s.seq)
                .ToArray();

            return res;
        }

        static string[] GetBabs(this string ip, string[] abas)
        {
            var res = ip.Skip(2).Select((c, i) =>
                    new
                    {
                        seq = new string(new[] { ip[i], ip[++i], c })
                    })
                .Where(s => (s.seq[0] == s.seq[2] && s.seq[0] != s.seq[1]) && abas.Any(a => a[0] == s.seq[1] && a[1] == s.seq[0] ))
                .Select(s => s.seq)
                .ToArray();

            return res;
        }
    }
}
