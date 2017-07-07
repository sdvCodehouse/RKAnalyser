using System;
using System.Linq;
using NUnit.Framework;
using RunkeeperAnalyser.Domain;

namespace RunkeeperAnalyser.Tests.Domain
{
    [TestFixture]
    public class DomainTests
    {
        [Test]
        public void CanCreateATrackPoint()
        {
            var sut = new TrackPoint();

            Assert.That(sut, Is.Not.Null);
        }

        [Test]
        public void WhenMandatoryPropertiesSet_CanCallTheseProperties()
        {
            var sut = new TrackPoint();
            sut.Latitude = 0.1234;
            sut.Longitude = 1.2345;
            sut.Elevation = 24;
            var nowTime = DateTime.UtcNow;
            sut.Time = nowTime;

            Assert.That(sut.Latitude, Is.EqualTo(0.1234));
            Assert.That(sut.Longitude, Is.EqualTo(1.2345));
            Assert.That(sut.Elevation, Is.EqualTo(24));
            Assert.That(sut.Time, Is.EqualTo(nowTime));
        }

        [Test]
        public void CanSetMultiplePointsToATrackSegment()
        {
            var sut = new TrackSegment();

            sut.TrackPoints = new[] {new TrackPoint(), new TrackPoint()};

            Assert.That(sut.TrackPoints.Count(), Is.EqualTo(2));
        }

        [Test]
        public void CanSetMultipleTrackSegmentsToTrack()
        {
            var sut = new ExerciseSession();

            sut.TrackSegments = new[] {new TrackSegment(), new TrackSegment()};

            Assert.That(sut.TrackSegments.Count(), Is.EqualTo(2));
        }


    }
}
