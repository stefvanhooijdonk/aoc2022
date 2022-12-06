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
            var marker = FindMarker(buffer, 4);
            Console.WriteLine(
                "Start-of-packet  marker found at index: {0} = {1} .",
                marker.Index,
                marker.Token);
            marker = FindMarker(buffer, 14);
            Console.WriteLine(
                "Start-of-message marker found at index: {0} = {1} .",
                marker.Index,
                marker.Token);
        }           

        public class Marker{
            private const string invalid = "invalid";
            public string Token = invalid;
            public int Index;
        }

        internal static Marker FindMarker(string buffer, int markerLength){
            for(var index = markerLength-1; index < buffer.Length; index++){                
                var marker = GetMarker(buffer,index,markerLength);
                if(marker.Distinct().Count() == markerLength){
                    return new Marker(){
                        Index = index+1, 
                        Token = string.Concat(marker)};
                }
            }
            return new Marker();
        }

        internal static List<char> GetMarker(string source, int index, int markerLength){
            var result = new List<char>();
            var marker = source.Substring(index-markerLength+1,markerLength);
            result.AddRange(marker.ToCharArray());
            return result;
        }
    }
}
