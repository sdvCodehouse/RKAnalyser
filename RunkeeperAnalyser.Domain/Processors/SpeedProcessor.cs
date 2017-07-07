using System;

namespace RunkeeperAnalyser.Domain.Processors
{
    public static class SpeedProcessor
    {
        internal static TimeSpan GetAverageSpeed(ExerciseSession exerciseSession)
        {
            TimeSpan duration = exerciseSession.Duration ?? new TimeSpan();
            return TimeSpan.FromMinutes(duration.TotalMinutes / exerciseSession.Distance);
        }

    }
}
