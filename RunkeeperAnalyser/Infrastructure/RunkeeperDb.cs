using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using RunkeeperAnalyser.Domain;

namespace RunkeeperAnalyser.Infrastructure
{
    public class RunkeeperDb : DbContext, IRunkeeperDataSource, IRunkeeperDb
    {
        public RunkeeperDb() : base("DefaultConnection")
        {
            
        }

        public IDbSet<ExerciseSession> ExerciseSessions { get; set; }
        public IDbSet<TrackSegment> TrackSegments { get; set; }
        public IDbSet<TrackPoint> TrackPoints { get; set; }

        IQueryable<ExerciseSession> IRunkeeperDataSource.ExerciseSessions
        {
            get { return ExerciseSessions; }
        }

        IQueryable<TrackSegment> IRunkeeperDataSource.TrackSegments
        {
            get { return TrackSegments; }
        }

        IQueryable<TrackPoint> IRunkeeperDataSource.TrackPoints
        {
            get { return TrackPoints; }
        }
    }
}