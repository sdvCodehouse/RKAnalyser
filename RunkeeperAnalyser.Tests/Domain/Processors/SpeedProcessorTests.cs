using System;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using RunkeeperAnalyser.Domain;
using RunkeeperAnalyser.Domain.Processors;

namespace RunkeeperAnalyser.Tests.Domain.Processors
{
    [TestFixture]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class SpeedProcessorTests
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
        public void SpeedProcessor_ActivityTypeRunning_SpeedInKmPerHr()
        {
            _runningSession.Speed = SpeedProcessor.GetAverageSpeed(_runningSession);

            Assert.That(_runningSession.Speed, Is.EqualTo(12d));
        }

        [Test]
        public void SpeedProcessor_ActivityTypeCycling_SpeedInKmPerHr()
        {
            _cyclingSession.Speed = SpeedProcessor.GetAverageSpeed(_cyclingSession);

            Assert.That(_cyclingSession.Speed, Is.EqualTo(24d));
        }
    }
}
