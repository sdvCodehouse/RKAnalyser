using System;
using System.Collections.Generic;
using RunkeeperAnalyser.Domain.Gpx;
using RunkeeperAnalyser.Domain.Processors;

namespace RunkeeperAnalyser.Domain
{
    public class ExerciseSession
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime? Time { get; set; }
        public virtual ICollection<TrackSegment> TrackSegments { get; set; }

        public virtual double Distance { get; set; }
        public virtual TimeSpan? Duration { get; set; }
        public virtual ActivityType ActivityType { get; set; }
        public virtual double? Elevation { get; set; }
        public virtual int Calories { get; set; }
        public virtual TimeSpan Pace { get; set; }
        public virtual double Speed { get; set; }

        public static ExerciseSession Create(GpxTrack track)
        {
            var exerciseSession = new ExerciseSession
            {
                Name = track.Name,
                Time = track.Time
            };
            ICollection<TrackSegment> trackSegments = new List<TrackSegment>();

            foreach (var gpxTrackSegment in track.Segments)
            {
                trackSegments.Add(TrackSegment.Create(gpxTrackSegment));

            }
            exerciseSession.Elevation = ElevationProcessor.SessionElevation(trackSegments);
            exerciseSession.Duration = DurationProcessor.SessionDuration(trackSegments);
            exerciseSession.Distance = DistanceProcessor.SessionDistance(trackSegments);
            exerciseSession.Calories = CaloriesProcessor.GetCaloriesBurned(exerciseSession);
            exerciseSession.Pace = PaceProcessor.GetAveragePace(exerciseSession);
            exerciseSession.ActivityType = ActivityProcessor.GetActivityType(exerciseSession);
            exerciseSession.Speed = SpeedProcessor.GetAverageSpeed(exerciseSession);
            exerciseSession.TrackSegments = trackSegments;

            return exerciseSession;
        }

    }
}


