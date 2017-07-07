using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using RunkeeperAnalyser.Domain;
using RunkeeperAnalyser.Filters;

namespace RunkeeperAnalyser.Tests.Domain
{
    [TestFixture]
    public class FilterTests
    {
        private List<ExerciseSession> _exerciseSessions;

        [SetUp]
        public void BeforeEachTest()
        {
            _exerciseSessions = new List<ExerciseSession>
            {
                new ExerciseSession {Time = new DateTime(2012, 1, 1, 1, 1, 1), Distance = 300},
                new ExerciseSession {Time = new DateTime(2014, 1, 1, 1, 1, 1), Distance = 500},
                new ExerciseSession {Time = new DateTime(2010, 1, 1, 1, 1, 1), Distance = 400},
                new ExerciseSession {Time = new DateTime(2011, 1, 1, 1, 1, 1), Distance = 100},
                new ExerciseSession {Time = new DateTime(2013, 1, 1, 1, 1, 1), Distance = 200}
            };
        }

        [Test]
        public void NullSortTermReturnsDateDescSort()
        {
            var sut = _exerciseSessions.RkOrderBy(null);

            Assert.That(sut.ToArray().First().Time.Value.Year, Is.EqualTo(2014)); 
            Assert.That(sut.ToArray().Last().Time.Value.Year, Is.EqualTo(2010));
        }

        [Test]
        public void DateAscSortTermReturnsCorrectSort()
        {
            var sut = _exerciseSessions.RkOrderBy("DateAsc");

            Assert.That(sut.ToArray().First().Time.Value.Year, Is.EqualTo(2010));
            Assert.That(sut.ToArray().Last().Time.Value.Year, Is.EqualTo(2014));
        }

        [Test]
        public void DateDescSortTermReturnsCorrectSort()
        {
            var sut = _exerciseSessions.RkOrderBy("DateDesc");

            Assert.That(sut.ToArray().First().Time.Value.Year, Is.EqualTo(2014));
            Assert.That(sut.ToArray().Last().Time.Value.Year, Is.EqualTo(2010));
        }

        [Test]
        public void DistanceSortTermReturnsCorrectSort()
        {
            var sut = _exerciseSessions.RkOrderBy("Distance");

            Assert.That(sut.ToArray().First().Distance, Is.EqualTo(100));
            Assert.That(sut.ToArray().Last().Distance, Is.EqualTo(500));
        }

    }
}
