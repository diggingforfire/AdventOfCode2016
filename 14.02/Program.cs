using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace _14._02
{
    class Program
    {
        static void Main()
        {
            var sw = new Stopwatch();
            sw.Start();

            const string salt = "qzyelonm";
            var keys = new List<string>();
            bool stop = false;
            int lastIndex = 0;

            for (int i = 0; i < int.MaxValue && !stop; i++)
            {
                var hash = GetMD5(salt + i);

                var triple = GetTriple(hash);

                if (triple != null)
                {
                    for (int j = i + 1; j <= 1000 + i; j++)
                    {
                        string qHash = GetMD5(salt + j);
                        var quintuple = GetQuintuple(qHash, triple[0]);

                        if (quintuple != null)
                        {
                            keys.Add(hash);

                            if (keys.Count == 64)
                            {
                                sw.Stop();
                                stop = true;
                                lastIndex = i;
                            }

                            break;
                        }
                    }
                }
            }

            Console.WriteLine($"Time elapsed: {sw.ElapsedMilliseconds}ms");
            Console.WriteLine(lastIndex);
            Console.ReadKey();
        }

        static readonly Dictionary<string, string> md5cache = new Dictionary<string, string>();

        static string GetTriple(string hash)
        {
            var triplets = hash.Skip(2).Select((c, index) => new string(new[] { hash[index], hash[++index], c }))
              .Where(seq => seq.Distinct().Count() == 1).ToList();

            return triplets.FirstOrDefault();
        }

        static string GetQuintuple(string hash, char tripleValue)
        {
            var quint = hash.Skip(4).Select((c, index) => new string(new[] { hash[index], hash[++index], hash[++index], hash[++index], c }))
                .Where(seq => seq.Distinct().Count() == 1 && seq.Distinct().First() == tripleValue).ToList();

            return quint.FirstOrDefault();
        }

        static string GetMD5(string input)
        {
            if (!md5cache.ContainsKey(input))
            {
                var md5 = GetMD5WithStretching(input);

                md5cache.Add(input, md5);
            }

            return md5cache[input];
        }

        private static string GetMD5WithStretching(string input)
        {
            var bytes = MD5.Create().ComputeHash(Encoding.Default.GetBytes(input));
            var md5 = string.Join("", bytes.Select(b => b.ToString("X2"))).ToLowerInvariant();

            for (int i = 0; i < 2016; i++)
            {
                bytes = MD5.Create().ComputeHash(Encoding.Default.GetBytes(md5));
                md5 = string.Join("", bytes.Select(b => b.ToString("X2"))).ToLowerInvariant();
            }

            return md5;
        }
    }
}
