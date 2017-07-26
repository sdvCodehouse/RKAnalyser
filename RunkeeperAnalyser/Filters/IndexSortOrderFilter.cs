﻿using System;
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
    }

    
}