using System.Collections.Generic;
using System.Linq;

namespace Mazes
{
    public static class MazeSolvers
    {
        public static IReadOnlyDictionary<Cell, int> SolveWithDijkstra(Cell startingCell)
        {
            var distances = new Dictionary<Cell, int>();
            var frontier = new HashSet<Cell>();

            frontier.Add(startingCell);
            var startingDistance = 0;

            while (frontier.Any())
            {
                // need to create new HashSet and swap after iteration in order to avoid error from modifying collection while iterating over it
                var newFrontier = new HashSet<Cell>();
                foreach (var frontierCell in frontier)
                {
                    distances.Add(frontierCell, startingDistance);
                    var unvisitedLinks = frontierCell.Links.Where(linkedCell => !distances.ContainsKey(linkedCell));
                    newFrontier.UnionWith(unvisitedLinks);
                }
                frontier = newFrontier;

                startingDistance++;
            }

            return distances;
        }
    }
}
