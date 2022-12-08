using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace aoc
{
    class Program
    {
        static void Main(string[]args)
        {
            Console.WriteLine("AdventOfCode 2022 - day 8.");

            var inputFolder = Environment.CurrentDirectory;

            Console.WriteLine("Reading input.txt from: {0}\r\n", inputFolder);
            var lines = System
                .IO
                .File
                .ReadAllLines(System.IO.Path.Combine(inputFolder, "input.txt"));
            Console.WriteLine("Read input.txt with: {0} lines.\r\n", lines.LongLength);

            var rows = new List<RowOfTrees>();

            foreach(var line in lines){

                var row = new RowOfTrees();
                foreach(var c in line){
                    row.Trees.Add(new Tree(c));
                }
                rows.Add(row);
            }

            var visibleTreeCount = 0;

            Console.WriteLine("Read input.txt with: {0} Rows of trees.\r\n", rows.Count);
            var rowlength = rows[0].Trees.Count;
            var rowcount = rows.Count;

            for(int x=0; x<rowlength;x++){
                for(int y=0;y<rowcount;y++){
                    if(IsVisible(x,y,rows)) visibleTreeCount++;

                }
            }

            Console.WriteLine("Found {0} visible trees.\r\n", visibleTreeCount);

            var highScore = 0;
            for(int x=0; x<rowlength;x++){
                for(int y=0;y<rowcount;y++){
                    rows[y].Trees[x].ScenicScore = ScenicScore(x,y,rows);
                    if(rows[y].Trees[x].ScenicScore > highScore){  
                        highScore=rows[y].Trees[x].ScenicScore;
                    }
                }
            }

            Console.WriteLine("Found {0} as highest Scenic Score.\r\n", highScore);
        }

        internal static bool IsVisible(int x, int y, List<RowOfTrees> rows){
            var tree = rows[y].Trees[x];

            if(y==0) return true;
            if(x==0) return true;
            if(y==rows.Count-1) return true;
            if(x==rows[0].Trees.Count-1) return true;

            var goLeftMaxHeight = rows[y].Trees.Where((tree,index)=> index<x).Max(tree => tree.Height);
            if(goLeftMaxHeight<tree.Height) return true;

            var goRightMaxHeight = rows[y].Trees.Where((tree,index)=> index>x).Max(tree => tree.Height);
            if(goRightMaxHeight<tree.Height) return true;

            var trees = new List<Tree>();
            rows.ForEach(row => trees.Add(row.Trees[x]));

            var goUpMaxHeight = trees.Where((tree,index)=> index < y).Max(tree => tree.Height);
            if(goUpMaxHeight<tree.Height) return true;

            var goDownMaxHeight = trees.Where((tree,index)=> index > y).Max(tree => tree.Height);
            if(goDownMaxHeight<tree.Height) return true;

            return false;
        }

        internal static int ScenicScore(int x, int y, List<RowOfTrees> rows){
            var tree = rows[y].Trees[x];
            int scoreLookingLeft = 0;
            int scoreLookingRight = 0;
            int scoreLookingUp = 0;
            int scoreLookingDown = 0;

            for(var left=x-1 ; left>=0 ; left-- ){
                scoreLookingLeft = x - left;
                if(rows[y].Trees[left].Height >= tree.Height) break;
            }

            for(var right=x+1;right < rows[y].Trees.Count ; right++){
                scoreLookingRight = right - x;
                if(rows[y].Trees[right].Height >= tree.Height) break;
            }

            for(var up=y-1 ; up >= 0 ; up--){
                scoreLookingUp= y - up;
                if(rows[up].Trees[x].Height >= tree.Height) break;
            }

            for(var down = y+1; down < rows.Count ; down++){ 
                scoreLookingDown = down - y;
                if(rows[down].Trees[x].Height >= tree.Height) break;
            }

            return scoreLookingLeft * scoreLookingRight * scoreLookingUp * scoreLookingDown;
        }
    }

    public class RowOfTrees{
        public List<Tree> Trees = new List<Tree>();
    }

    public class Tree{
        public Tree(char height){            
            Height = int.Parse(height.ToString());
        }
        public int Height = 0;

        public int ScenicScore = 0;
    }
}
