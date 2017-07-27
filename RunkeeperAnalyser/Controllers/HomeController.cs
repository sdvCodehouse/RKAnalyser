using System;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using RunkeeperAnalyser.Domain;
using RunkeeperAnalyser.Domain.Gpx;
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

        public ActionResult Index(string sortTerm = null, int page = 1)
        {
            var indexVm = new IndexViewModel(Request.QueryString);

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

        public ActionResult Import()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Upload()
        {
            if (Request.Files !=null && Request.Files.Count > 0)
            {
                for (int index = 0; index < Request.Files.Count; index++)
                {
                    HttpPostedFileBase file = Request.Files[index];
                    if (file != null && file.FileName.EndsWith(".gpx"))
                    {
                        LoadContentsOfGpxFile(file);
                    }
                }
            }

            return RedirectToAction("Index");
        }

        private void LoadContentsOfGpxFile(HttpPostedFileBase file)
        {
            try
            {
                GpxTrack track = GpxEngine.GetGpxTrackFromFile(file);
                if (track != null)
                {
                    ExerciseSession session = ExerciseSession.Create(track);
                    _db.ExerciseSessions.AddOrUpdate(s => s.Name, session);
                    _db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                //todo need to add logging
                // go to next file
            }
            
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}
