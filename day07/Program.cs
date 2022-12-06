using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace aoc
{
    class Program
    {
        static void Main(string[]args)
        {
            Console.WriteLine("AdventOfCode 2022 - day 7.");

            var inputFolder = Environment.CurrentDirectory;

            Console.WriteLine("Reading input.txt from: {0}\r\n", inputFolder);
            var lines = System
                .IO
                .File
                .ReadAllLines(System.IO.Path.Combine(inputFolder, "input.txt"));
            Console.WriteLine("Read input.txt with: {0} lines.\r\n", lines.LongLength);
        }
    }
}
