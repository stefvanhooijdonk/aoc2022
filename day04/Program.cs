using System;
using System.Collections;

namespace aoc
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AdventOfCode 2022 - day 4.");

            var inputFolder = Environment.CurrentDirectory;

            Console.WriteLine("Reading input.txt from: {0}\r\n",inputFolder);
            var lines = System.IO.File.ReadAllLines(System.IO.Path.Combine(inputFolder,"input.txt"));
            Console.WriteLine("Read input.txt with: {0} lines.\r\n",lines.LongLength);

            List<CleaningPair> list = new List<CleaningPair>();
            foreach(var l in lines){
                list.Add(new CleaningPair(l));
            }
            Console.WriteLine("Containing CleaningPairs {0}.\r\n",list.Where(cleaningpair => cleaningpair.FullyContains()).Count());
            Console.WriteLine("Overlapping CleaningPairs {0}.\r\n",list.Where(cleaningpair => cleaningpair.Overlaps()).Count());
        }
    }

    public class CleaningPair
    {
        public CleaningPair(string line){
            section1 = new SectionRange(line.Split(",")[0]);
            section2 = new SectionRange(line.Split(",")[1]);
        }

        internal SectionRange section1;
        internal SectionRange section2;

        public bool FullyContains() {         
            return section1.Contains(section2) || section2.Contains(section1);
        }
        public bool Overlaps() {         
            return section1.Overlaps(section2) || FullyContains();
        }
    }

    public class SectionRange{
        public SectionRange(string section){
            start = int.Parse(section.Split("-")[0]);
            end = int.Parse(section.Split("-")[1]);
        }

        internal int start =0;
        internal int end = 0;

        public bool Contains(SectionRange range){
            return start <= range.start && end >= range.end;
        }
        public bool Overlaps(SectionRange range){
            return  (start <= range.start && range.start <= end) ||
                    (start <= range.end && range.end <= end);
        }
    }
}
