using System;
using System.Data.Entity.Migrations;
using System.Web;
using System.Web.Mvc;
using RunkeeperAnalyser.Domain;
using RunkeeperAnalyser.Domain.Gpx;
using RunkeeperAnalyser.Infrastructure;

namespace RunkeeperAnalyser.Controllers
{
    public class ImportController : Controller
    {
        private IRunkeeperDb _db;

        public ImportController(IRunkeeperDb db)
        {
            _db = db;
        }

        //
        // GET: /Import/
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload()
        {
            if (Request.Files != null && Request.Files.Count > 0)
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

            return RedirectToAction("Index", "Home");
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
