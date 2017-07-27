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
                case "SpeedAsc":
                    return orderBy.OrderBy(s => s.Speed);

                case "SpeedDesc":
                    return orderBy.OrderByDescending(s => s.Speed);

                case "DistanceAsc":
                    return orderBy.OrderBy(s => s.Distance);

                case "DistanceDesc":
                    return orderBy.OrderByDescending(s => s.Distance);

                case "DateAsc":
                    return orderBy.OrderBy(s => s.Time);

                default:
                    return orderBy.OrderByDescending(s => s.Time);
            }
        }

        public static IEnumerable<T> DistanceRange<T>(this IEnumerable<T> where, int? distanceFrom, int? distanceTo)
            where T : ExerciseSession
        {
            if (distanceFrom == null || distanceTo == null)
            {
                return where.Where(s => true);
            }
            return where.Where(s => s.Distance >= distanceFrom && s.Distance <= distanceTo);
        }

        public static IEnumerable<T> DateRange<T>(this IEnumerable<T> where, DateTime? dateFrom, DateTime? dateTo)
            where T : ExerciseSession
        {
            if (dateFrom == null || dateTo == null)
            {
                return where.Where(s => true);
            }
            return where.Where(s => s.Time >= dateFrom && s.Time <= dateTo);
        }

        public static IEnumerable<T> ForActivities<T>(this IEnumerable<T> where, IEnumerable<string> activities)
            where T : ExerciseSession
        {
            IEnumerable<T> newWhere = where;
            if (activities == null || activities.First() == "showAll")
            {
                return where.Where(s => true);
            }

            return where.Where(s => activities.Contains(((int) s.ActivityType).ToString()));
        }
    }

    
}