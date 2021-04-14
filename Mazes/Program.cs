using System;

namespace Mazes
{
    class Program
    {
        static void Main(string[] args)
        {
            var numRows = 10;
            var numCols = 10;

            Console.WriteLine("Maze with no connections:");
            var unconnectedMaze = new Grid(numRows, numCols);
            MazePrinters.PrintMazeToTerminal(unconnectedMaze);
            Console.WriteLine();

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();
        }
    }
}
