using System;
using System.IO;
using System.Linq;

namespace _08._01
{
    class Program
    {
        private static readonly int renderIntervalInMs = 100;
        private static readonly int rowLength = 50;
        private static readonly int colLength = 6;
        private static readonly bool[][] grid = new bool[colLength][];

        static void Main()
        {
            for (int i = 0; i < grid.Length; i++)
                grid[i] = new bool[rowLength];

            Render();

            File.ReadAllLines("input.txt").Select(line => line.Split(' ')).ToList().ForEach(parts =>
            {
                if (parts[0] == "rect")
                    Enumerable.Range(0, int.Parse(parts[1].Split('x')[0])).ToList().ForEach(x =>
                    Enumerable.Range(0, int.Parse(parts[1].Split('x')[1])).ToList().ForEach(y => grid[y][x] = true));
                else if (parts[0] == "rotate" && parts[1] == "row")
                {
                    int row = int.Parse(parts[2].Split('=')[1]);
                    int offset = int.Parse(parts[4].ToString());

                    var on = grid[row].Select((value, x) => new {value, x}).Where(v => v.value).ToList();
                    grid[row].Select((value, x) => new { value, x }).Where(v => v.value).ToList().ForEach(v => grid[row][v.x] = false);
                    on.ForEach(o => grid[row][(o.x + offset)%rowLength] = true);
                }
                else if (parts[0] == "rotate" && parts[1] == "column")
                {
                    int col = int.Parse(parts[2].Split('=')[1].ToString());
                    int offset = int.Parse(parts[4].ToString());

                    var on = grid.Select(row => row[col]).Select((value, y) => new {value, y}).Where(v => v.value).ToList();
                    grid.Select(row => row[col]).Select((value, y) => new { value, y }).ToList().ForEach(v => grid[v.y][col] = false);
                    on.ForEach(o => grid[(o.y + offset) % colLength][col] = true);
                }
            });

            Render();

            Console.WriteLine("");
            Console.WriteLine("Pixels on: " + grid.SelectMany(b => b).Sum(b => b ? 1 : 0));
            Console.ReadKey();
        }

        static void Render()
        {
            Console.Clear();

            for (int y = 0; y < grid.Length; y++)
            {
                for (int x = 0; x < grid[0].Length; x++)
                    Console.Write((grid[y][x] ? '#' : ' ') + " ");

                Console.WriteLine();
            }

        }
    }
}
