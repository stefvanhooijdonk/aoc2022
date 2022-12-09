using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace aoc
{
    class Program
    {
        static void Main(string[]args)
        {
            Console.WriteLine("AdventOfCode 2022 - day 9.");

            var inputFolder = Environment.CurrentDirectory;

            Console.WriteLine("Reading input.txt from: {0}\r\n", inputFolder);
            var lines = System
                .IO
                .File
                .ReadAllLines(System.IO.Path.Combine(inputFolder, "input.txt"));
            Console.WriteLine("Read input.txt with: {0} lines.\r\n", lines.LongLength);       

            var steps = new List<Step>();
            foreach(var l in lines){
                steps.Add(new Step(l));
            }    

            Console.WriteLine("Read {0} steps.\r\n", steps.Count); 

            var head = new SnakeElement(0,0);
            var tail = new SnakeElement(0,0);
            var snake = new List<SnakeElement>();
            snake.Add(head);  //1
            snake.Add(tail);  //2
            snake.Add(new SnakeElement(0,0));
            snake.Add(new SnakeElement(0,0));
            snake.Add(new SnakeElement(0,0));
            snake.Add(new SnakeElement(0,0));
            snake.Add(new SnakeElement(0,0));
            snake.Add(new SnakeElement(0,0));
            snake.Add(new SnakeElement(0,0));
            snake.Add(new SnakeElement(0,0));

            var listCoordinatesVisited = new List<Coordinate>();
            listCoordinatesVisited.Add(new Coordinate(0,0));

            foreach(var step in steps){

                // move the head step by step
                for(var stepIncrement=0; stepIncrement < step.Size ;stepIncrement++){
                    switch(step.StepDirection){
                        case Direction.Up:
                            head.Y++;
                            break;
                        case Direction.Down:
                            head.Y--;
                            break;
                        case Direction.Left:
                            head.X--;
                            break;
                        case Direction.Right:
                            head.X++;
                            break;
                    }
                    if(!Touching(head,tail)){
                        // position the tail based on the new head position
                        MoveTail(head,tail);    

                        // this is to track the first problem/tail
                        //listCoordinatesVisited.Add(new Coordinate(tail.X,tail.Y));
                    }

                    // walk the snake and propagate the step just taken allong the snake
                    // skip the head of this snake. already processed just above this 
                    for(var snakeIndex=1;snakeIndex<snake.Count-1;snakeIndex++){
                        var s1 = snake[snakeIndex];
                        var s2 = snake[snakeIndex+1];
                        if(!Touching(s1,s2)){
                        // position the tail based on the new head position
                            MoveTail(s1,s2);    
                        }
                    } 
                    // this is to track the second problem/last tail
                    listCoordinatesVisited.Add(new Coordinate(snake[9].X,snake[9].Y));
                }               
            }

            var count = listCoordinatesVisited.Distinct(new CoordinateComparer()).Count();
            Console.WriteLine("Head is at {0} {1}.\r\n", head.X, head.Y);
            // problem 1
            Console.WriteLine("Tail is at {0} {1}.\r\n", tail.X, tail.Y);
            // problem 2:
            Console.WriteLine("Tail is at {0} {1}.\r\n", snake[9].X, snake[9].Y);

            // problem 2:
            Console.WriteLine("Coordinates visited {0}.\r\n",count);
        }

        internal static void CheckSnake(){

        }

        internal static bool Touching(SnakeElement head, SnakeElement tail){
            return Math.Abs(head.X-tail.X)<=1 && Math.Abs(head.Y-tail.Y)<=1;
        }

        internal static void MoveTail(SnakeElement head, SnakeElement tail){
            // assume not touching

            if(head.Y == tail.Y){
                if(head.X > tail.X + 1) tail.X++;
                if(head.X < tail.X - 1) tail.X--;

                return;
            }
            if(head.X == tail.X){
                if(head.Y > tail.Y + 1) tail.Y++;
                if(head.Y < tail.Y - 1) tail.Y--;

                return;
            }
            var xdiff = head.X-tail.X; // either 1 or 2 or -1 -2
            var ydiff = head.Y-tail.Y; // either 1 or 2 or -1 -2

            // quadrant 1 / make diagonal step
            if(xdiff>0 && ydiff>0){
                tail.X++;
                tail.Y++;
                return;
            }
            // quadrant 2 / make diagonal step
            if(xdiff>0 && ydiff<0){
                tail.X++;
                tail.Y--;
                return;
            }
            // quadrant 3 / make diagonal step
            if(xdiff<0 && ydiff>0){
                tail.X--;
                tail.Y++;
                return;
            }
            // quadrant 4 / make diagonal step
            if(xdiff<0 && ydiff<0){
                tail.X--;
                tail.Y--;
                return;
            }
        }
    }

    public class Coordinate{
         public Coordinate(int x, int y){
            X = x;
            Y = y;
        }

        public int X {get;set;}
        public int Y {get;set;}
    }

    public class SnakeElement:Coordinate{
        public SnakeElement(int x, int y):base(x,y){
            X=x;
            Y=y;
        }
    }

    public class Step{
        public Step(string input){
            direction = (Direction)input.Split(" ")[0][0];
            size = int.Parse(input.Split(" ")[1]);
        }

        private Direction direction = Direction.Up;
        private int size = 0;

        public int Size {
            get{
                return size;
            }
        }
        public Direction StepDirection{
            get {
                return direction;
            }
        } 
    }

    public enum Direction{
        Up = 'U',
        Down = 'D',
        Left = 'L',
        Right = 'R'
    }

    public class CoordinateComparer : IEqualityComparer<Coordinate>
    {  
        // Products are equal if their names and product numbers are equal.
        public bool Equals(Coordinate x, Coordinate y)
        {

            //Check whether the compared objects reference the same data.
            if (Object.ReferenceEquals(x, y)) return true;

            //Check whether any of the compared objects is null.
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            //Check whether the products' properties are equal.
            return x.X == y.X && x.Y == y.Y;
        }

        // If Equals() returns true for a pair of objects
        // then GetHashCode() must return the same value for these objects.

        public int GetHashCode(Coordinate coordinate)
        {
            //Check whether the object is null
            if (Object.ReferenceEquals(coordinate, null)) return 0;


            //Get hash code for the Coordinate field.
            int hashX = coordinate.X.GetHashCode();
            int hashY = coordinate.Y.GetHashCode();

            //Calculate the hash code for the Coordinate.
            return hashX ^ hashY;
        }
    }
}
