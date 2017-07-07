using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using NUnit.Framework;
using RunkeeperAnalyser.Controllers;
using RunkeeperAnalyser.Domain;
using RunkeeperAnalyser.Infrastructure;

namespace RunkeeperAnalyser.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        private FakeDb _fakeDb = null;

        [SetUp]
        public void beforeEachTest()
        {
            _fakeDb = new FakeDb();
            _fakeDb.ExerciseSessions = new FakeDbSet<ExerciseSession>();
        }

        [Test]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController(_fakeDb);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController(_fakeDb);

            // Act
            ViewResult result = controller.Import() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController(_fakeDb);

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }

    internal class FakeDb : IRunkeeperDb
    {
        public IDbSet<ExerciseSession> ExerciseSessions { get; set; }
        public IDbSet<TrackSegment> TrackSegments { get; set; }
        public IDbSet<TrackPoint> TrackPoints { get; set; }
        public int SaveChanges()
        {
            throw new System.NotImplementedException();
        }


        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }

    internal class FakeDbSet<T> : IDbSet<T> where T : class
    {
        ObservableCollection<T> _data;
        IQueryable _query;

        public FakeDbSet()
        {
            _data = new ObservableCollection<T>();
            _query = _data.AsQueryable();
        }

        public virtual T Find(params object[] keyValues)
        {
            throw new NotImplementedException("Derive from FakeDbSet<T> and override Find");
        }

        public T Add(T item)
        {
            _data.Add(item);
            return item;
        }

        public T Remove(T item)
        {
            _data.Remove(item);
            return item;
        }

        public T Attach(T item)
        {
            _data.Add(item);
            return item;
        }

        public T Detach(T item)
        {
            _data.Remove(item);
            return item;
        }

        public T Create()
        {
            return Activator.CreateInstance<T>();
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        public ObservableCollection<T> Local
        {
            get { return _data; }
        }

        Type IQueryable.ElementType
        {
            get { return _query.ElementType; }
        }

        System.Linq.Expressions.Expression IQueryable.Expression
        {
            get { return _query.Expression; }
        }

        IQueryProvider IQueryable.Provider
        {
            get { return _query.Provider; }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return _data.GetEnumerator();
        }
    }
}
