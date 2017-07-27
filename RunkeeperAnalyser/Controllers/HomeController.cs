using System.Web.Mvc;
using PagedList;
using RunkeeperAnalyser.Filters;
using RunkeeperAnalyser.Infrastructure;
using RunkeeperAnalyser.ViewModels;

namespace RunkeeperAnalyser.Controllers
{
    public class HomeController : Controller
    {
        private IRunkeeperDb _db;

        public HomeController(IRunkeeperDb db)
        {
            _db = db;
        }

        public ActionResult Index(int page = 1)
        {
            var indexVm = Request != null ? new IndexViewModel(Request.QueryString) : new IndexViewModel();

            var allExerciseSessions = _db.ExerciseSessions
                .DistanceRange(indexVm.DistanceFrom, indexVm.DistanceTo)
                .DateRange(indexVm.DateFrom, indexVm.DateTo)
                .RkOrderBy(indexVm.SortTerm)
                .ToPagedList(page, 20); // todo make page size configurable

            indexVm.ExerciseSessions = allExerciseSessions;

            if (Request != null && Request.IsAjaxRequest())
            {
                return PartialView("_ExerciseSessions", indexVm.ExerciseSessions);
            }

            return View(indexVm);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}
