using System;

namespace Mazes
{
    class Program
    {
        static void Main(string[] args)
        {
            var numRows = 10;
            var numColumns = 10;

            Console.WriteLine("Maze with no connections:");
            var unconnectedMaze = new Grid(numRows, numColumns);
            MazePrinters.PrintMazeToTerminal(unconnectedMaze);
            Console.WriteLine();

            Console.WriteLine("Maze generated with binary tree algorithm:");
            var binaryTreeMaze = MazeGenerators.BinaryTreeMaze(numRows, numColumns);
            MazePrinters.PrintMazeToTerminal(binaryTreeMaze);
            Console.WriteLine();

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();
        }
    }
}
