using System;
using System.Drawing;
using System.Linq;

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

            Console.WriteLine("Previous maze, solved with Dijkstra's algorithm");
            var distances = MazeSolvers.SolveWithDijkstra(binaryTreeMaze.Cells[0, 0]);
            MazePrinters.PrintMazeToTerminal(binaryTreeMaze, cell => distances[cell]);
            Console.WriteLine();

            Console.WriteLine("Previous maze, colored and labeled");
            MazePrinters.PrintMazeToTerminal(binaryTreeMaze, cell => distances[cell], distance => ConvertDistanceToColor(distance, distances.Values.Max()));

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();
        }

        private static Color ConvertDistanceToColor(int distance, int maxDistance)
        {
            var proportion = 1 - (((double) distance) / maxDistance);
            var intensity = Convert.ToByte(Math.Floor(proportion * 255));
            return Color.FromArgb(intensity, intensity, intensity);
        }
    }
}
