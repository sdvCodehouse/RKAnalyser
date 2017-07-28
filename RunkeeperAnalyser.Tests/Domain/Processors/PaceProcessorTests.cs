using System;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using RunkeeperAnalyser.Domain;
using RunkeeperAnalyser.Domain.Processors;

namespace RunkeeperAnalyser.Tests.Domain.Processors
{
    [TestFixture]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class PaceProcessorTests
    {
        private ExerciseSession _runningSession;
        private ExerciseSession _cyclingSession;

        [OneTimeSetUp]
        public void OnceBeforeTests()
        {
            _runningSession = new ExerciseSession { ActivityType = ActivityType.Running };
            _runningSession.Duration = new TimeSpan(0, 0, 30, 0);
            _runningSession.Distance = 6;

            _cyclingSession = new ExerciseSession { ActivityType = ActivityType.Cycling };
            _cyclingSession.Duration = new TimeSpan(0, 0, 30, 0);
            _cyclingSession.Distance = 12;
        }

        [Test]
        public void PaceProcessor_ActivityTypeRunning_PaceInMinsPerKm()
        {
            _runningSession.Pace = PaceProcessor.GetAveragePace(_runningSession);

            Assert.That(_runningSession.Pace, Is.EqualTo(new TimeSpan(0,0,5,0)));
        }

        [Test]
        public void PaceProcessor_ActivityTypeCycling_PaceInMinsPerKm()
        {
            _cyclingSession.Pace = PaceProcessor.GetAveragePace(_cyclingSession);

            Assert.That(_cyclingSession.Pace, Is.EqualTo(new TimeSpan(0, 0, 2, 30)));
        }
    }
}
