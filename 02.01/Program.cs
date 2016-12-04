using System;
using System.IO;
using System.Linq;

namespace _02._01
{
    class Program
    {
        static void Main(string[] args)
        {
            int pos = 5, min = 1, max = 9, gridSize = 3;
      
            var code = File.ReadAllLines("input.txt").Aggregate("", (a, b) =>
            {
                b.Select(c => c).ToList().ForEach(c =>
                {
                    pos +=  (c == 'U' && pos - gridSize >= min) ? -gridSize :
                            (c == 'D' && pos + gridSize <= max) ? gridSize :
                            (c == 'L' && pos % gridSize != min) ? -min :
                            (c == 'R' && pos % gridSize >= min) ? min : 0;
                });
 
                return a + pos.ToString();
            });

            Console.WriteLine(code);
            Console.ReadKey();
        }
    }
}
