using System;

namespace RunkeeperAnalyser.Domain.Processors
{
    public static class SpeedProcessor
    {
        public static double GetAverageSpeed(ExerciseSession exerciseSession)
        {
            TimeSpan duration = exerciseSession.Duration ?? new TimeSpan();
            return exerciseSession.Distance / duration.TotalHours;
        }
    }
}
