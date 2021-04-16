using System;
using System.Drawing;
using System.Linq;

namespace Mazes
{
    class Program
    {
        static void Main(string[] args)
        {
            var rowRadius = 10;
            var columnRadius = 10;
            var numRows = 2 * rowRadius + 1;
            var numColumns = 2 * columnRadius + 1;

            Console.WriteLine("Maze generated with binary tree algorithm:");
            var binaryTreeMaze = MazeGenerators.BinaryTreeMaze(numRows, numColumns);
            MazePrinters.PrintMazeToTerminal(binaryTreeMaze);
            Console.WriteLine();

            Console.WriteLine("Previous maze, colored based on Dijkstra's algorithm");
            var binaryTreeDistances = MazeSolvers.SolveWithDijkstra(binaryTreeMaze.Cells[rowRadius, columnRadius]);   // start in center cell
            MazePrinters.PrintMazeToTerminal(binaryTreeMaze, cell => binaryTreeDistances[cell], distance => ConvertDistanceToColor(distance, binaryTreeDistances.Values.Max()));

            Console.WriteLine("Maze generated with sidewinder algorithm:");
            var sidewinderMaze = MazeGenerators.SidewinderMaze(numRows, numColumns);
            MazePrinters.PrintMazeToTerminal(sidewinderMaze);
            Console.WriteLine();

            Console.WriteLine("Previous maze, colored based on Dijkstra's algorithm");
            var sidewinderDistances = MazeSolvers.SolveWithDijkstra(sidewinderMaze.Cells[rowRadius, columnRadius]);   // start in center cell
            MazePrinters.PrintMazeToTerminal(sidewinderMaze, cell => sidewinderDistances[cell], distance => ConvertDistanceToColor(distance, sidewinderDistances.Values.Max()));



            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();
        }

        private static Color ConvertDistanceToColor(int distance, int maxDistance)
        {
            var proportion = 1 - (((double) distance) / maxDistance);
            var darkIntensity = Convert.ToByte(Math.Floor(proportion * 255));
            var brightIntensity = Convert.ToByte(Math.Floor((proportion * 127) + 128));
            return Color.FromArgb(darkIntensity, brightIntensity, darkIntensity);
        }
    }
}
