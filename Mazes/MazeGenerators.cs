using System;
using System.Collections.Generic;
using System.Linq;

namespace Mazes
{
    public static class MazeGenerators
    {
        public static Grid BinaryTreeMaze(int numRows, int numColumns)
        {
            var rng = new Random();
            var maze = new Grid(numRows, numColumns);

            for (var row = 0; row < maze.NumRows; row++)
            {
                for (var column = 0; column < maze.NumColumns; column++)
                {
                    var cell = maze.Cells[row, column];

                    var potentialNeighbor = 
                        new List<Cell?>
                        {
                            cell.North,
                            cell.East,
                        }
                        .OfType<Cell>() // filter out nulls
                        .Sample(rng);

                    if (potentialNeighbor != null)
                    {
                        cell.Link(potentialNeighbor);
                    }
                }
            }

            return maze;
        }

        public static Grid SidewinderMaze(int numRows, int numColumns)
        {
            var rng = new Random();
            var maze = new Grid(numRows, numColumns);

            for (var row = 0; row < maze.NumRows; row++)
            {
                var run = new List<Cell>();

                for (var column = 0; column < maze.NumColumns; column++)
                {
                    var cell = maze.Cells[row, column];
                    run.Add(cell);

                    var isEasternBoundary = cell.East == null;
                    var isNorthernBoundary = cell.North == null;

                    var shouldCloseOut = isEasternBoundary || (!isNorthernBoundary && rng.Next(2) == 0);
                    if (shouldCloseOut)
                    {
                        var memberToLinkNorth = run.Sample(rng);
                        if (memberToLinkNorth?.North != null)
                        {
                            memberToLinkNorth.Link(memberToLinkNorth.North);
                        }
                        run.Clear();
                    }
                    else if (cell.East != null) // keep run going
                    {
                        cell.Link(cell.East);
                    }
                }
            }

            return maze;
        }
    }
}
