using System;
using RunkeeperAnalyser.Domain.Gpx;

namespace RunkeeperAnalyser.Domain
{
    public class TrackPoint
    {
        public int Id { get; set; }
        public virtual double Latitude { get; set; }
        public virtual double Longitude { get; set; }
        public virtual double? Elevation { get; set; }
        public virtual DateTime? Time { get; set; }

        public virtual int TrackSegmentId { get; set; }

        public static TrackPoint Create(GpxTrackPoint gpxTrackPoint)
        {
            var trackPoint = new TrackPoint
            {
                Time = gpxTrackPoint.Time,
                Elevation = gpxTrackPoint.Elevation,
                Latitude = gpxTrackPoint.Latitude,
                Longitude = gpxTrackPoint.Longitude
            };
            return trackPoint;
        }
    }
}