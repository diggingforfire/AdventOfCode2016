using System;
using System.Linq;

namespace _01._02
{
    static class Program
    {
        enum Direction { N, E, S, W }
        static void Main(string[] args)
        {
            Direction d = Direction.N;

            var trail = new[] { new { X = 0, Y = 0 } }.ToDictionary(p => new { p.X, p.Y }, p => 0);

            try
            {
                "L2, L5, L5, R5, L2, L4, R1, R1, L4, R2, R1, L1, L4, R1, L4, L4, R5, R3, R1, L1, R1, L5, L1, R5, L4, R2, L5, L3, L3, R3, L3, R4, R4, L2, L5, R1, R2, L2, L1, R3, R4, L193, R3, L5, R45, L1, R4, R79, L5, L5, R5, R1, L4, R3, R3, L4, R185, L5, L3, L1, R5, L2, R1, R3, R2, L3, L4, L2, R2, L3, L2, L2, L3, L5, R3, R4, L5, R1, R2, L2, R4, R3, L4, L3, L1, R3, R2, R1, R1, L3, R4, L5, R2, R1, R3, L3, L2, L2, R2, R1, R2, R3, L3, L3, R4, L4, R4, R4, R4, L3, L1, L2, R5, R2, R2, R2, L4, L3, L4, R4, L5, L4, R2, L4, L4, R4, R1, R5, L2, L4, L5, L3, L2, L4, L4, R3, L3, L4, R1, L2, R3, L2, R1, R2, R5, L4, L2, L1, L3, R2, R3, L2, L1, L5, L2, L1, R4"
                .Split(',')
                .Select(i => i.Trim())
                .Aggregate(new { X = 0, Y = 0 }, (a, b) =>
                {
                    d = d.Turn(b[0]);
                    int blocks = int.Parse(b.Substring(1));
                    int deltaX = blocks * (d == Direction.E ? 1 : d == Direction.W ? -1 : 0);
                    int deltaY = blocks * (d == Direction.N ? 1 : d == Direction.S ? -1 : 0);
                    int newX = a.X + deltaX;
                    int newY = a.Y + deltaY;

                    Enumerable.Range(0, Math.Abs(deltaX) + Math.Abs(Math.Sign(deltaX))).Skip(1).ToList().ForEach(x =>
                    {
                        int tmpX = a.X + (x * Math.Sign(deltaX));
                        try { trail.Add(new { X = tmpX, Y = a.Y }, 0); } catch { Console.WriteLine(Math.Abs(tmpX) + a.Y); throw; }
                    });

                    Enumerable.Range(0, Math.Abs(deltaY) + Math.Abs(Math.Sign(deltaY))).Skip(1).ToList().ForEach(y =>
                    {
                        int tmpY = a.Y + (y * Math.Sign(deltaY));
                        try { trail.Add(new { X = a.X, Y = tmpY }, 0); } catch { Console.WriteLine(Math.Abs(tmpY) + a.X); throw; }

                    });

                    return new { X = newX, Y = newY };
                });
            }
            catch
            {
                Console.ReadKey();
            }
        }

        static Direction Turn(this Direction directionType, char direction)
        {
            return (Direction)((((int)directionType) + (direction == 'L' ? -1 : 1) + 4) % 4);
        }
    }
}
