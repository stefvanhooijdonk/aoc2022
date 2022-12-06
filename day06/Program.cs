using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace aoc
{
    class Program
    {
        static void Main(string[]args)
        {
            Console.WriteLine("AdventOfCode 2022 - day 6.");

            var inputFolder = Environment.CurrentDirectory;

            Console.WriteLine("Reading input.txt from: {0}\r\n", inputFolder);
            var lines = System
                .IO
                .File
                .ReadAllLines(System.IO.Path.Combine(inputFolder, "input.txt"));
            Console.WriteLine("Read input.txt with: {0} lines.\r\n", lines.LongLength);

            var buffer = lines[0];

            for(var index=3;index<buffer.Length;index++){                
                var marker = buffer.Substring(index-3,4);
                if(CheckCharsAreDifferent(marker)){
                    Console.WriteLine("Start-of-packet Marker: {0} position: {1}", marker, index + 1);
                    break;
                }
            }
            for(var index=13;index<buffer.Length;index++){                
                var marker = buffer.Substring(index-13,14);
                if(CheckCharsAreDifferent(marker)){
                    Console.WriteLine("Start of Message marker: {0} position: {1}", marker, index + 1);
                    break;
                }
            }
        }           

        public static int Count(string input, char substr)
        {
            return Regex.Matches(input, substr.ToString()).Count;
        }

        internal static bool CheckCharsAreDifferent(string marker){

            var result = true;
            for(var i = 0; i<marker.Length;i++){
                result = result && Count(marker, marker[i]) == 1;
                if(!result) break;
            }
            return result;
        }
    }
}
