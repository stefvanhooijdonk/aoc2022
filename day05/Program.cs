using System;
using System.Collections;

namespace aoc
{
    class Program
    {
        static void Main(string[]args)
        {
            Console.WriteLine("AdventOfCode 2022 - day 5.");

            var inputFolder = Environment.CurrentDirectory;

            Console.WriteLine("Reading input.txt from: {0}\r\n", inputFolder);
            var lines = System
                .IO
                .File
                .ReadAllLines(System.IO.Path.Combine(inputFolder, "input.txt"));
            Console.WriteLine("Read input.txt with: {0} lines.\r\n", lines.LongLength);

            List<Stack> stacks = new List <Stack>();
            var lineindex = 0;

            var column=1;
            while(column<36){
                var stack = new Stack();
                for (lineindex = 0; lineindex < lines.LongLength; lineindex++) {
                    if(lineindex==8){
                        break;
                    }
                    var line = lines[lineindex];
                    if(column<line.Length){
                        var columnvalue = line[column];
                        if(!string.IsNullOrEmpty(columnvalue.ToString().Trim())){
                            var crate = new Crate();
                            crate.Id = columnvalue;
                            stack.Crates.Add(crate);
                        }
                    }
                }
                stack.Id = stacks.Count+1;
                stacks.Add(stack);
                lineindex = 0;
                column=column+4;
            }

            Console.WriteLine("Read stacks: {0}.\r\n", stacks.Count);

            List<Movement> movements = new List<Movement>();
            for (lineindex = 10; lineindex < lines.LongLength; lineindex++) {
                movements.Add(new Movement(lines[lineindex]));
            }

            Console.WriteLine("Read movements: {0}.\r\n", movements.Count);

            foreach(var movement in movements){
                ApplyMovement(stacks, movement);
            }

            foreach(var stack in stacks){
                Console.WriteLine("Read movements: {0} {1} {2}.\r\n", stack.Id, stack.Crates.Count, stack.Crates[0].Id);
            }
        }        

        public static void ApplyMovement(List<Stack> stacks, Movement movement){

            for(var movesteps = 0; movesteps< movement.Count; movesteps++){

                var takeCrate = stacks[movement.IndexFrom].Crates[0];
                
                // day05 - step 1
                //stacks[movement.IndexTo].Crates.Insert(0,takeCrate);
                
                // day05 - step 2
                stacks[movement.IndexTo].Crates.Insert(movesteps,takeCrate);
                
                stacks[movement.IndexFrom].Crates.RemoveAt(0);
            }
        }
    }


    public class Stack {

        public int Id;
        public List < Crate > Crates = new List < Crate > ();
    }

    public class Crate {
        public char Id;
    }

    public class Movement {
        public Movement(string input) {
            var splits = input.Split(" ");
            Count = int.Parse(splits[1]);
            IndexFrom = int.Parse(splits[3]) - 1;
            IndexTo = int.Parse(splits[5]) - 1;
        }

        public int Count;
        public int IndexFrom;
        public int IndexTo;
    }
}
