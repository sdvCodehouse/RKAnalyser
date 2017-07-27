namespace RunkeeperAnalyser.Domain
{
    public enum ActivityType
    {
        Unknown,
        Running,
        Cycling
    }

    /// <summary>
    /// This class is used within razor pages which does not allow the (int) cast in page.
    /// </summary>
    public static class ActivityTypeExtensions
    {
        public static int GetValue(this ActivityType activityType)
        {
            return (int)activityType;
        }
    }
}
