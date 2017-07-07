using System;
using System.Collections.Generic;
using System.Linq;
using RunkeeperAnalyser.Domain;

namespace RunkeeperAnalyser.Filters
{
    public static class IndexSortOrderFilter
    {
        public static IOrderedEnumerable<T> RkOrderBy<T>(this IEnumerable<T> orderBy, string sortTerm) where T : ExerciseSession
        {
            switch (sortTerm)
            {
                case "Distance":
                    return orderBy.OrderBy(s => s.Distance);

                case "DateAsc":
                    return orderBy.OrderBy(s => s.Time);

                default:
                    return orderBy.OrderByDescending(s => s.Time);
            }
        }
    }
}