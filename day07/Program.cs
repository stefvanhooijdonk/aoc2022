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


            // parse commands/output
            var root = new Devicedirectory("/");
            var currentDirectory = root;
            foreach(var commandoutputline in lines){
                if(commandoutputline=="$ cd /"){
                    // ignore
                }
                else{
                    if(commandoutputline.StartsWith("$")){
                        // read command

                        if(commandoutputline == "$ cd .."){
                            currentDirectory = currentDirectory?.Parent;
                        }
                        else {
                            if(commandoutputline == "$ ls"){
                                // ignore
                            }
                            else{
                                // cd subdir
                                var goToSubDir = commandoutputline.Split(" ")[2];
                                currentDirectory = currentDirectory?.Directories.Single(
                                    d => d.Name == goToSubDir);
                            }
                        }
                    }
                    else{
                        // read ls output
                        if(commandoutputline.StartsWith("dir")){
                            currentDirectory?.AddSubdirectory(commandoutputline);
                        }
                        else{
                            // read file input
                            currentDirectory?.Files.Add( new Devicefile(commandoutputline));
                        }
                    }
                }
            }

            Console.WriteLine("Total size:{0}",root.Size);

            var dirs = root.GetMaxDir(100000);
            Console.WriteLine("Total larger dir size:{1} (in {0} dirs)",
                dirs.Count, 
                dirs.Sum(d=>d.Size));

            var totalSize = 70000000;
            var minSpaceNeeded = 30000000;
            var spaceFree = totalSize - root.Size;
            dirs = root.GetMinDir(minSpaceNeeded-spaceFree);
            var selectedDir = dirs.OrderBy(d=>d.Size).First();

             Console.WriteLine("Space needed:{1} and by deleting {2} we free {0}",
                selectedDir.Size, 
                minSpaceNeeded-spaceFree,
                selectedDir.Name);
        }
    }

    public class Devicedirectory {
        public Devicedirectory(string name){
            Name = name;     
        }

        public Devicedirectory(string name, Devicedirectory parent){
            Name = name;
            Parent = parent;
        }

        public string Name;

        public Devicedirectory? Parent;

        public void AddSubdirectory(string input) {
            var newdirname = input.Split(" ")[1];   
            var newsubdir= new Devicedirectory(newdirname, this);
            this.Directories.Add(newsubdir);
        }

        public List<Devicedirectory> Directories = new List<Devicedirectory>();
        public List<Devicefile> Files = new List<Devicefile>();

        public int Size {
            get {
                int result = Files.Sum(f => f.Size);
                result += Directories.Sum(d=> d.Size);
                return result;
            }
        }

        public List<Devicedirectory> GetMaxDir(int maxSize) {
            var result = new List<Devicedirectory>();
            Directories.ForEach(d => result.AddRange(d.GetMaxDir(maxSize)));
            result.AddRange( Directories.Where( d => d.Size < maxSize ));
            return result;
        }

         public List<Devicedirectory> GetMinDir(int minSize) {
            var result = new List<Devicedirectory>();
            Directories.ForEach(d => result.AddRange(d.GetMinDir(minSize)));
            result.AddRange( Directories.Where( d => d.Size > minSize ));
            return result;
        }
    }

    public class Devicefile {

        public Devicefile(string name, int size){
            Name = name;
            Size = size;
        }
        
        public Devicefile(string input){
            var splitinput = input.Split(" ");
            Name = splitinput[1];
            Size = int.Parse(splitinput[0]);
        }

        public string Name;

        public int Size;
    }
}
