using System;
using System.Drawing;
using System.Linq;
using System.Text;
using static Crayon.Output;

namespace Mazes
{
    public static class MazePrinters
    {
        private static readonly string Base36Chars = "0123456789abcdefghijklmnopqrstuvwxyz";

        public static void PrintMazeToTerminal(Grid maze, Func<Cell, int>? getCellDistance = null, Func<int, Color>? colorCell = null)
        {
            var corner = "+";
            var horizontalWall = "---";
            var verticalWall = "|";
            var horizontalOpening = "   ";
            var verticalOpening = " ";

            var output = new StringBuilder();

            var topWall = string.Join(horizontalWall, Enumerable.Repeat(corner, maze.NumColumns + 1));
            output.AppendLine(topWall);

            for (var row = 0; row < maze.NumRows; row++)
            {
                var middleLine = new StringBuilder();
                var bottomLine = new StringBuilder();

                // west boundary of maze
                middleLine.Append(verticalWall);
                bottomLine.Append(corner);

                for (var column = 0; column < maze.NumColumns; column++)
                {
                    var cell = maze.Cells[row, column];

                    string cellContents;
                    if (getCellDistance != null)
                    {
                        var distance = getCellDistance(cell);

                        if (colorCell != null)
                        {
                            var color = colorCell(distance);
                            var colorBlock = Rgb(color.R, color.G, color.B).Text("\u2588"); // U+2558 = full block
                            cellContents = string.Concat(Enumerable.Repeat(colorBlock, 3));    
                        }
                        else
                        {
                            cellContents = $" {ConvertToBase36Char(distance)} ";
                        }
                    }
                    else
                    {
                        cellContents = "   ";
                    }

                    middleLine.Append(cellContents);
                    middleLine.Append(cell.IsLinked(cell.East) ? verticalOpening : verticalWall);

                    bottomLine.Append(cell.IsLinked(cell.South) ? horizontalOpening : horizontalWall);
                    bottomLine.Append(corner);
                }

                output.AppendLine(middleLine.ToString());
                output.AppendLine(bottomLine.ToString());
            }

            Console.WriteLine(output.ToString());
        }

        private static char ConvertToBase36Char(int num)
        {
            if (num < 0 || num > Base36Chars.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(num));
            }

            return Base36Chars[num];
        }
    }
}
