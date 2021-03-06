﻿using System;

namespace RunkeeperAnalyser.Domain.Processors
{
    public static class CaloriesProcessor
    {
        internal static int GetCaloriesBurned(ExerciseSession exerciseSession)
        {
            // met is the measure of calorie burn rate for a given activity
            double met = 11; // default set as met for running at 6.7mph (9 min miles)
            double weight = 90;
            TimeSpan duration = exerciseSession.Duration ?? new TimeSpan();

            if (exerciseSession.ActivityType == ActivityType.Cycling)
            {
                met = 10; // this is the met for cycling at between 14 - 15.5 mph
            }

            return Convert.ToInt32(Math.Round(CalculateCalories(met, duration.TotalMinutes, weight), MidpointRounding.AwayFromZero));

        }

        private static double CalculateCalories(double met, double minutes, double weight)
        {
            return 0.0175 * met * minutes * weight;
        }
    }
}
