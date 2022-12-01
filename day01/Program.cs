using System;
using System.Collections;

namespace aoc
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AdventOfCode 2022 - day 1.");

            var inputFolder = Environment.CurrentDirectory;

            Console.WriteLine("Reading input.txt from: {0}\r\n",inputFolder);
            var lines = System.IO.File.ReadAllLines(System.IO.Path.Combine(inputFolder,"input.txt"));
            Console.WriteLine("Read input.txt with: {0} lines.\r\n",lines.LongLength);

            List<Elf> elfs = new List<Elf>();
            Elf currentElf = new Elf();

            foreach(string line in lines){
                if(string.IsNullOrEmpty(line)){
                    elfs.Add(currentElf);
                    currentElf = new Elf();
                }
                else{
                    currentElf.Items.Add(new Item(line));
                }
            }

            elfs.Add(currentElf);

            Console.WriteLine("Read input.txt with: {0} elfs.\r\n",elfs.Count);

            int elfWithMostCalories = elfs.Max(e=>e.TotalCalories);

            Console.WriteLine("elfWithMostCalories.TotalCalories {0} calories.\r\n",elfWithMostCalories);

            var orderdelfs = elfs.OrderByDescending(e => e.TotalCalories).Take(3).ToList();

            int calores = orderdelfs[0].TotalCalories +  orderdelfs[1].TotalCalories +  orderdelfs[2].TotalCalories;
            Console.WriteLine("Top 3 TotalCalories {0} calories.\r\n",calores);
        }
    }

    public class Elf
    {

        public List<Item> Items = new List<Item>();

        public int TotalCalories{
            get{
                return Items.Sum(i=>i.Calories);
            }
        }
    }

    public class Item {

        private int calories = 0;

        public Item(string input)
        {
            try{
                calories = int.Parse(input);
            }
            catch(Exception){
                // wrong input
            }
        }

        public int Calories{
            get{return calories;}
        }
    }
}
