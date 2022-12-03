using System;
using System.Collections;

namespace aoc
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AdventOfCode 2022 - day 3.");

            var inputFolder = Environment.CurrentDirectory;

            Console.WriteLine("Reading input.txt from: {0}\r\n",inputFolder);
            var lines = System.IO.File.ReadAllLines(System.IO.Path.Combine(inputFolder,"input.txt"));
            Console.WriteLine("Read input.txt with: {0} lines.\r\n",lines.LongLength);

            List<Rucksack> rucksacks = new List<Rucksack>();

            foreach(string line in lines){
                var item = new Rucksack(line);
                rucksacks.Add(item);
            }

            Console.WriteLine("Sum or priorities for compartment rucksacks: {0}.\r\n",
                rucksacks.Sum(i=> PriorityOfItem(i.DoubleItem)));

            List<RucksackGroup> groups = new List<RucksackGroup>();

            var group = new RucksackGroup();
            foreach(string line in lines){
                group.sacks.Add(new Rucksack(line));

                if(group.sacks.Count>2){
                    groups.Add(group);
                    group = new RucksackGroup();
                }
            }

            Console.WriteLine("Sum or priorities for group: {0}.\r\n",
                groups.Sum(g => PriorityOfItem(g.ItemInAllRucksacks)));
        }

        internal const string alfabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static int PriorityOfItem(char item){
            return 1+alfabet.IndexOf(item);
        }
    }

    public class RucksackGroup{
        public List<Rucksack> sacks = new List<Rucksack>();

        public char ItemInAllRucksacks{
            get{

                return sacks.First().All.Where(
                    i => sacks[1].All.Contains(i) && 
                         sacks[2].All.Contains(i)
                    ).ToList().First();
            }
        }    
   }

    public class Rucksack{
        
        public Rucksack(string items){
            if(!string.IsNullOrEmpty(items)){
                All = items.ToCharArray().ToList();
                CompartmentOne = items.Substring(0,items.Length/2).ToCharArray().ToList();
                CompartmentTwo = items.Substring(items.Length/2).ToCharArray().ToList();
            }
        }

        public char DoubleItem{
            get{
                return CompartmentOne.Where(i => CompartmentTwo.Contains(i)).ToList().First();
            }
        }      

        public List<char> CompartmentOne = new List<char>();
        public List<char> CompartmentTwo = new List<char>();

        public List<char> All = new List<char>();
    }
}
