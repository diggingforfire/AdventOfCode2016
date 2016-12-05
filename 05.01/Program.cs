using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05._01
{
    static class Program
    {
        static void Main()
        {
            var sw = new Stopwatch();
            sw.Start();

            object syncRoot = new object();
            string input = "reyedfim";
            string output = "";

            Parallel.ForEach(Enumerable.Range(0, int.MaxValue), (i, state) =>
            {
                var md5 = $"{input}{i}".ToMD5();
                if (md5.StartsWith("00000"))
                {
                    lock (syncRoot)
                    {
                        output += md5[5];
                        if (output.Length == 8)
                            state.Break();
                    }
                }
            });

            sw.Stop();

            Console.WriteLine($"Running time: {sw.Elapsed.TotalSeconds}s");
            Console.WriteLine(output);
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
