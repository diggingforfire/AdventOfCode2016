using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05._02
{
    static class Program
    {
        static void Main()
        {
            var sw = new Stopwatch();
            sw.Start();

            object syncRoot = new object();
            string input = "reyedfim";
            var sb = new StringBuilder("        ", 8);

            Parallel.ForEach(Enumerable.Range(0, int.MaxValue), (i, state) =>
            {
                var md5 = $"{input}{i}".ToMD5();
                if (md5.StartsWith("00000"))
                {
                    int pos;
                    if (int.TryParse(md5[5].ToString(), out pos))
                    {
                        lock (syncRoot)
                        {
                            if (pos < 8 && sb[pos] == ' ')
                            {
                                sb[pos] = md5[6];
                                if (!sb.ToString().Contains(" "))
                                    state.Break();
                            }
                        }
                    }
                }
            });

            sw.Stop();

            Console.WriteLine($"Running time: {sw.Elapsed.TotalSeconds}s");
            Console.WriteLine(sb);
            Console.ReadKey();
        }

        public static string ToMD5(this string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();

                foreach (byte b in hashBytes)
                    sb.Append(b.ToString("X2"));

                return sb.ToString();
            }
        }
    }
}