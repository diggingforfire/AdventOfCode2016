using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _12._01
{
	class Program
	{
		static void Main()
		{
			var registers = new Dictionary<char, int>
			{
				{'a', 0}, {'b', 0}, {'c', 0}, {'d', 0}
			};

			int placeholder;

			var instructions = File.ReadAllLines("input.txt").Select(line => line.Split(' ')).ToList();

			Func<string, int> getValue = (instruction) => int.TryParse(instruction, out placeholder) ? placeholder : registers[instruction[0]];

			Func<string[], int> process = (instruction) =>
			{
				int address = instructions.IndexOf(instruction);

				switch (instruction[0])
				{
					case "cpy":
						registers[instruction[2][0]] = getValue(instruction[1]);
						address++;
						break;
					case "inc":
						registers[instruction[1][0]]++;
						address++;
						break;
					case "dec":
						registers[instruction[1][0]]--;
						address++;
						break;
					case "jnz":
						int op1 = getValue(instruction[1]);
						int op2 = getValue(instruction[2]);
						address += op1 == 0 ? 1 : op2;
						break;
				}

				return address;
			};

			int next = process(instructions[0]);

			while (next < instructions.Count && next >= 0)
				next = process(instructions[next]);

			Console.WriteLine(registers['a']);
			Console.ReadKey();
		}
	}
}