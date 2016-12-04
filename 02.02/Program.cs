using System;
using System.IO;
using System.Linq;

namespace _02._02
{
    class Program
    {
        static void Main(string[] args)
        {
            int pos = 5;

            var keypad = new[]
            {
                new int?[] {null,   null,   null,   null,   null,   null,   null},
                new int?[] {null,   null,   null,   1,      null,   null,   null},
                new int?[] {null,   null,   2,      3,      4,      null,   null},
                new int?[] {null,   5,      6,      7,      8,      9,      null},
                new int?[] {null,   null,   10,     11,     12,     null,   null},
                new int?[] {null,   null,   null,   13,     null,   null,   null},
                new int?[] {null,   null,   null,   null,   null,   null,   null},
            };

            var code = File.ReadAllLines("input.txt").Aggregate("", (a, b) =>
            {
                b.Select(c => c).ToList().ForEach(c =>
                {
                    var index = keypad.Select((arr, row) => new { arr, row }).Where(i => i.arr.Contains(pos)).Select(i => new { i.row, col = Array.IndexOf(i.arr, pos) }).Single();

                    pos = (c == 'U') ? keypad[index.row - 1][index.col] ?? pos :
                          (c == 'D') ? keypad[index.row + 1][index.col] ?? pos :
                          (c == 'L') ? keypad[index.row][index.col - 1] ?? pos :
                          (c == 'R') ? keypad[index.row][index.col + 1] ?? pos : 0;

                });

                return a + pos.ToString("X");
            });

            Console.WriteLine(code);
            Console.ReadKey();
        }
    }
}
