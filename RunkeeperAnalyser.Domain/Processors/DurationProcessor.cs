using System;
using System.Collections.Generic;

namespace RunkeeperAnalyser.Domain.Processors
{
    public static class DurationProcessor
    {
        internal static TimeSpan? SegmentDuration(IList<TrackPoint> trackPoints)
        {
            TimeSpan? duration = new TimeSpan();
            if (trackPoints.Count > 1)
            {
                duration = trackPoints[trackPoints.Count - 1].Time - trackPoints[0].Time;
            }
            return duration;
        }

        internal static TimeSpan? SessionDuration(ICollection<TrackSegment> trackSegments)
        {
            TimeSpan? duration = new TimeSpan();
            if (trackSegments != null && trackSegments.Count > 0)
            {
                foreach (var trackSegment in trackSegments)
                {
                    duration += trackSegment.Duration;
                }
            }
            return duration;
        }

    }
}
