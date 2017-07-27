using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NUnit.Framework;
using RunkeeperAnalyser.ViewModels;

namespace RunkeeperAnalyser.Tests.ViewModels
{
    [TestFixture]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class IndexViewModelTests
    {
        [Test]
        public void Activities_Null_QueryString()
        {
            IndexViewModel vm = new IndexViewModel(null);

            Assert.IsNotNull(vm);
            Assert.IsNotNull(vm.Activities);
            Assert.That(vm.Activities.First(), Is.EqualTo("showAll"));
        }

        [Test]
        public void ActivitiesLoad_QueryStringHasActivities()
        {
            NameValueCollection queryString = new NameValueCollection {{"activities", "showAll,1"}};
            IndexViewModel vm = new IndexViewModel(queryString);

            Assert.That(vm.Activities.Count(), Is.EqualTo(2));
            Assert.That(vm.Activities.Last(), Is.EqualTo("1"));
        }
    }

}
