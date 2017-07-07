using System;
using System.Data.Entity;
using RunkeeperAnalyser.Domain;

namespace RunkeeperAnalyser.Infrastructure
{
    public interface IRunkeeperDb : IDisposable
    {
        IDbSet<ExerciseSession> ExerciseSessions { get; set; }
        IDbSet<TrackSegment> TrackSegments { get; set; }
        IDbSet<TrackPoint> TrackPoints { get; set; }

        int SaveChanges();
    }

}