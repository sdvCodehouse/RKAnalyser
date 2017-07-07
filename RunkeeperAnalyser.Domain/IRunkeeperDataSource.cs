using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunkeeperAnalyser.Domain
{
    public interface IRunkeeperDataSource
    {
        IQueryable<ExerciseSession> ExerciseSessions { get; }
        IQueryable<TrackSegment> TrackSegments { get; }
        IQueryable<TrackPoint> TrackPoints { get; }

    }
}
