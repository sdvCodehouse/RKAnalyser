using System;
using System.Collections.Generic;
using RunkeeperAnalyser.Domain.Gpx;
using RunkeeperAnalyser.Domain.Processors;

namespace RunkeeperAnalyser.Domain
{
    public class TrackSegment
    {
        public int Id { get; set; }
        public virtual ICollection<TrackPoint> TrackPoints { get; set; }

        public virtual int ExerciseSessionId { get; set; }

        public virtual double Distance { get; set; }
        public virtual TimeSpan? Duration { get; set; }
        public virtual double? Elevation { get; set; }
        public virtual int Calories { get; set; }

        public static TrackSegment Create(GpxTrackSegment gpxTrackSegment)
        {
            var trackSegment = new TrackSegment();
            List<TrackPoint> trackPoints = new List<TrackPoint>();
            foreach (var point in gpxTrackSegment.TrackPoints)
            {
                trackPoints.Add(TrackPoint.Create(point));
            }
            trackSegment.TrackPoints = trackPoints;
            trackSegment.Elevation = ElevationProcessor.SegmentElevation(trackPoints);
            trackSegment.Duration = DurationProcessor.SegmentDuration(trackPoints);
            trackSegment.Distance = DistanceProcessor.SegmentDistance(trackPoints);

            return trackSegment;
        }

    }
}