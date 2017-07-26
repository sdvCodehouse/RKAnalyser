namespace RunkeeperAnalyser.Domain.Processors
{
    public static class ActivityProcessor
    {
        public static ActivityType GetActivityType(ExerciseSession exerciseSession)
        {
            if (exerciseSession != null && !string.IsNullOrWhiteSpace(exerciseSession.Name))
            {
                // to do consider using reg from settings for matching, in order to extend to other gpx types
                // update: in fact the regex pattern should be part of the gpx setup, with a seperate gpx template
                // for each type of import. E.g. Strava, Garmin, Runkeeper, etc
                if (exerciseSession.Name.Contains("Running")) return ActivityType.Run;
                if (exerciseSession.Name.Contains("Cycling")) return ActivityType.Cycle;
            }

            return ActivityType.Unknown;
        }
    }
}
 