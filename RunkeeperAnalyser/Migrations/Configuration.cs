using System.Linq;
using RunkeeperAnalyser.Domain;

namespace RunkeeperAnalyser.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Infrastructure.RunkeeperDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Infrastructure.RunkeeperDb context)
        {
            if (!context.ExerciseSessions.Any())
            {
                ExerciseSession track = new ExerciseSession()
                {
                    Name = "Test ExerciseSession",
                    Time = DateTime.UtcNow,
                    Pace = new TimeSpan(),
                    TrackSegments = new[]
                    {
                        new TrackSegment()
                        {
                            TrackPoints = new[]
                            {
                                new TrackPoint()
                                {
                                    Elevation = 24,
                                    Latitude = 52.0,
                                    Longitude = -0.1234,
                                    Time = DateTime.UtcNow
                                },
                                new TrackPoint()
                                {
                                    Elevation = 25,
                                    Latitude = 52.0,
                                    Longitude = -0.1242,
                                    Time = DateTime.UtcNow.AddSeconds(12)
                                },
                                new TrackPoint()
                                {
                                    Elevation = 23,
                                    Latitude = 52.01,
                                    Longitude = -0.1248,
                                    Time = DateTime.UtcNow.AddSeconds(22)
                                },
                            }
                        },
                        new TrackSegment()
                        {
                            TrackPoints = new[]
                            {
                                new TrackPoint()
                                {
                                    Elevation = 26,
                                    Latitude = 52.0,
                                    Longitude = -0.1234,
                                    Time = DateTime.UtcNow.AddSeconds(32)
                                },
                                new TrackPoint()
                                {
                                    Elevation = 26,
                                    Latitude = 52.0,
                                    Longitude = -0.1242,
                                    Time = DateTime.UtcNow.AddSeconds(36)
                                },
                                new TrackPoint()
                                {
                                    Elevation = 25,
                                    Latitude = 52.01,
                                    Longitude = -0.1248,
                                    Time = DateTime.UtcNow.AddSeconds(40)
                                },
                            }
                        }

                    }
                };
                context.ExerciseSessions.AddOrUpdate(t => t.Name, track);
            }
            
        }
    }
}
