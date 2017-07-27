using System;

namespace RunkeeperAnalyser.Domain.Processors
{
    public static class PaceProcessor
    {
        public static TimeSpan GetAveragePace(ExerciseSession exerciseSession)
        {
            TimeSpan duration = exerciseSession.Duration ?? new TimeSpan();
            return TimeSpan.FromMinutes(duration.TotalMinutes / exerciseSession.Distance);
        }
    }
}
