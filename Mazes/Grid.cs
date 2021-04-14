using System;
using System.Collections.Generic;
using System.Linq;

namespace Mazes
{
    public class Cell
    {
        public int Row { get; }
        public int Column { get; }

        // neighbors - *not necessarily connected*
        public Cell? North { get; set; }
        public Cell? South { get; set; }
        public Cell? East { get; set; }
        public Cell? West { get; set; }

        /// <summary>
        /// All neighboring cells, whether connected or not.
        /// </summary>
        public IReadOnlyList<Cell> Neighbors
        {
            get
            {
                var neighbors = new List<Cell?>
                {
                    North,
                    South,
                    East,
                    West,
                };
                return neighbors.OfType<Cell>().ToList().AsReadOnly();
            }
        }

        /// <summary>
        /// Connected neighboring cells.
        /// </summary>
        public HashSet<Cell> Links { get; } = new();

        public Cell(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public void Link(Cell otherCell, bool isBidirectional = true)
        {
            Links.Add(otherCell);
            if (isBidirectional)
            {
                otherCell.Link(this, false);
            }
        }

        public void Unlink(Cell otherCell, bool isBidirectional = true)
        {
            Links.Remove(otherCell);
            if (isBidirectional)
            {
                otherCell.Unlink(this, false);
            }
        }

        public bool IsLinked(Cell? otherCell)
        {
            if (otherCell == null)
            {
                return false;
            }

            return Links.Contains(otherCell);
        }

        public override bool Equals(object? obj)
        {
            return obj is Cell otherCell
                && Row == otherCell.Row
                && Column == otherCell.Column;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Column);
        }
    }

    public class Grid
    {
        public Cell[,] Cells { get; }
        private readonly Random rng = new();

        public int NumRows
        {
            get
            {
                return Cells.GetLength(0);
            }
        }

        public int NumColumns
        {
            get
            {
                return Cells.GetLength(1);
            }
        }

        public Cell? this[int row, int column]
        {
            get
            {
                if (row < 0 || row > NumRows - 1)
                {
                    return null;
                }

                if (column < 0 || column > NumColumns - 1)
                {
                    return null;
                }

                return Cells[row, column];
            }
        }

        public int NumCells
        {
            get
            {
                return NumRows * NumColumns;
            }
        }

        public Grid(int numRows, int numColumns)
        {
            // prepare grid
            Cells = new Cell[numRows, numColumns];

            for (var row = 0; row < NumRows; row++)
            {
                for (var column = 0; column < NumColumns; column++)
                {
                    Cells[row, column] = new Cell(row, column);
                }
            }

            // configure cells
            // needs to be a separate loop so all cells are already initialized as we set up neighbors
            for (var row = 0; row < NumRows; row++)
            {
                for (var column = 0; column < NumColumns; column++)
                {
                    var cell = Cells[row, column];
                    cell.North = this[row - 1, column];
                    cell.South = this[row + 1, column];
                    cell.West = this[row, column - 1];
                    cell.East = this[row, column + 1];
                }
            }
        }

        public Cell GetRandomCell()
        {
            var row = rng.Next(NumRows);
            var column = rng.Next(NumColumns);
            return Cells[row, column];  // use Cells instead of indexer to guarantee non-nullability
        }
    }
}
