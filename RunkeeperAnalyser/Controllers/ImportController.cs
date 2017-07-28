using System;
using System.Data.Entity.Migrations;
using System.Web;
using System.Web.Mvc;
using log4net;
using RunkeeperAnalyser.Domain;
using RunkeeperAnalyser.Domain.Gpx;
using RunkeeperAnalyser.Infrastructure;
using RunkeeperAnalyser.ViewModels;

namespace RunkeeperAnalyser.Controllers
{
    public class ImportController : Controller
    {
        private IRunkeeperDb _db;
        private static ILog _log;

        public ImportController(IRunkeeperDb db)
        {
            _log = LogManager.GetLogger(GetType());
            _db = db;
        }

        //
        // GET: /Import/
        [HttpGet]
        public ActionResult Index()
        {
            return View(new ImportViewModel());
        }

        [HttpPost]
        public ActionResult Upload()
        {
            var importViewModel = new ImportViewModel {ImportExecuted = true};

            if (Request.Files != null && Request.Files.Count > 0)
            {
                for (int index = 0; index < Request.Files.Count; index++)
                {
                    HttpPostedFileBase file = Request.Files[index];
                    if (file != null && file.FileName.EndsWith(".gpx"))
                    {
                        LoadContentsOfGpxFile(file, importViewModel);
                    }
                }
            }
            return View("Index", importViewModel);
        }

        private void LoadContentsOfGpxFile(HttpPostedFileBase file, ImportViewModel importViewModel)
        {
            try
            {
                GpxTrack track = GpxEngine.GetGpxTrackFromFile(file);
                if (track != null)
                {
                    ExerciseSession session = ExerciseSession.Create(track);
                    _db.ExerciseSessions.AddOrUpdate(s => s.Name, session);
                    _db.SaveChanges();
                    importViewModel.FilesImported++;
                }
                else
                {
                    importViewModel.FilesFailed.Add(new FailedFile
                    {
                        Filename = file.FileName,
                        ErrorMessage = "File could not be processed."
                    });
                }
            }
            catch (Exception e)
            {
                _log.Error(string.Format("Failed to import file {0} with message: {1}", file.FileName, e.Message));
                importViewModel.FilesFailed.Add(new FailedFile
                {
                    Filename = file.FileName,
                    ErrorMessage = e.Message
                });
            }

        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}
