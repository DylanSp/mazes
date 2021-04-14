using System;
using System.Collections.Generic;
using System.Linq;

namespace Mazes
{
    public static class EnumerableExtensions
    {
        public static T? Sample<T>(this IEnumerable<T> elements, Random rng) where T : class
        {
            if (!elements.Any())
            {
                return null;
            }

            var index = rng.Next(elements.Count());
            return elements.ElementAt(index);
        }
    }
}
