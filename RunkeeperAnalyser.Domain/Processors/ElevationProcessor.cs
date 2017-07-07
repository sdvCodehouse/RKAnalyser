using System;
using System.Collections.Generic;

namespace RunkeeperAnalyser.Domain.Processors
{
    public static class ElevationProcessor
    {
        internal static double? SegmentElevation(IEnumerable<TrackPoint> trackPoints)
        {
            double? elevation = 0;
            TrackPoint previousTrackPoint = null;

            foreach (var trackPoint in trackPoints)
            {
                if (previousTrackPoint != null && previousTrackPoint.Elevation < trackPoint.Elevation)
                    elevation += Round(trackPoint.Elevation - previousTrackPoint.Elevation, 4);
                previousTrackPoint = trackPoint;
            }

            return elevation;
        }


        internal static double? SessionElevation(ICollection<TrackSegment> trackSegments)
        {
            double? elevation = 0;
            if (trackSegments != null && trackSegments.Count > 0)
            {
                foreach (var trackSegment in trackSegments)
                {
                    elevation += Round(trackSegment.Elevation, 4);
                }
            }
            return elevation;
        }


        private static double? Round(double? amount, int places)
        {
            double doubleAmount = amount ?? 0;

            return Math.Round(doubleAmount, places);
        }

    }
}
