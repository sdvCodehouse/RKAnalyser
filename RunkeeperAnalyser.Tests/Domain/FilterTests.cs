using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NUnit.Framework;
using RunkeeperAnalyser.Domain;
using RunkeeperAnalyser.Filters;
using RunkeeperAnalyser.ViewModels;

namespace RunkeeperAnalyser.Tests.Domain
{
    [TestFixture]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class FilterTests
    {
        private List<ExerciseSession> _exerciseSessions;

        [SetUp]
        public void BeforeEachTest()
        {
            _exerciseSessions = new List<ExerciseSession>
            {
                new ExerciseSession {Time = new DateTime(2012, 1, 1, 0, 0, 0), Distance = 300},
                new ExerciseSession {Time = new DateTime(2014, 1, 1, 0, 0, 0), Distance = 500},
                new ExerciseSession {Time = new DateTime(2010, 1, 1, 0, 0, 0), Distance = 400},
                new ExerciseSession {Time = new DateTime(2011, 1, 1, 0, 0, 0), Distance = 100},
                new ExerciseSession {Time = new DateTime(2013, 1, 1, 0, 0, 0), Distance = 200}
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

#region DistanceRange Tests

        [Test]
        public void DistanceRange_ReturnsAll_ForNullArguments()
        {
            var sut = _exerciseSessions.DistanceRange(null, null);

            Assert.That(sut.Count(), Is.EqualTo(5));
        }

        [Test]
        public void DistanceRange_ReturnsCorrectNumber_ForGoodArguments()
        {
            var sut = _exerciseSessions.DistanceRange(200, 400);

            Assert.That(sut.Count(), Is.EqualTo(3));
        }

        [Test]
        public void DistanceRange_ReturnsCorrectNumber_ForGoodNVCollection()
        {
            NameValueCollection collection = new NameValueCollection();
            collection.Add("DistanceFrom", "200");
            collection.Add("DistanceTo", "400");


            var indexVm = new IndexViewModel(collection);
            var sut = _exerciseSessions.DistanceRange(indexVm.DistanceFrom, indexVm.DistanceTo);

            Assert.That(sut.Count(), Is.EqualTo(3));
        }

        [Test]
        public void DistanceRange_ReturnsCorrectNumber_ForCaseInsensitiveNVCollection()
        {
            NameValueCollection collection = new NameValueCollection();
            collection.Add("distancefrom", "200");
            collection.Add("distanceto", "400");
            var indexVm = new IndexViewModel(collection);

            var sut = _exerciseSessions.DistanceRange(indexVm.DistanceFrom, indexVm.DistanceTo);

            Assert.That(sut.Count(), Is.EqualTo(3));
        }


        [Test]
        public void DistancRange_ReturnsAll_ForOneNullArgument()
        {
            var sut = _exerciseSessions.DistanceRange(null, 100);

            Assert.That(sut.Count(), Is.EqualTo(5));
        }

#endregion DistanceRange Tests

        #region DateRange Tests

        [Test]
        public void DateRange_ReturnsAll_ForNullArguments()
        {
            NameValueCollection collection = new NameValueCollection();

            var indexVm = new IndexViewModel(collection);

            var sut = _exerciseSessions.DateRange(indexVm.DateFrom, indexVm.DateFrom);

            Assert.That(sut.Count(), Is.EqualTo(5));
        }

        [Test]
        public void DateRange_ReturnsCorrectNumber_ForCaseInsensitiveNVCollection()
        {
            NameValueCollection collection = new NameValueCollection();
            collection.Add("datefrom", "1/1/2012");
            collection.Add("dateto", "1/1/2014");
            var indexVm = new IndexViewModel(collection);

            var sut = _exerciseSessions.DateRange(indexVm.DateFrom, indexVm.DateTo);

            Assert.That(sut.Count(), Is.EqualTo(3));
        }

        #endregion DateRange Tests
    }
}
