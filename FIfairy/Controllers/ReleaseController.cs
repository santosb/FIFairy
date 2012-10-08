using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using FIfairyDomain;

namespace FIfairy.Controllers
{
    public class ReleaseController : Controller
    {
        private readonly IReleaseRepository _releaseRepository;

        public ReleaseController(IReleaseRepository releaseRepository)
        {
            _releaseRepository = releaseRepository;
        }

        [HttpGet]
        public ViewResult Index()
        {
            return View("Release", _releaseRepository.GetReleases());
        }

        [HttpGet]
        public ViewResult ReleaseByDate(int year, int month, int day)
        {
            return View("Release", _releaseRepository.GetReleases(new DateTime(year, month, day)));
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View("CreateRelease");
        }       

        [HttpPost]
        public ActionResult Create(Release release, HttpPostedFileBase prepatfile)
        {
            try
            {                
                if (HasPrePatFile(prepatfile))
                {
                    _releaseRepository.SavePrePatEmailFile(prepatfile.FileName, prepatfile.InputStream);
                    release.PrePatEmailFileInfo = new PrePatEmailFileInfo { Name = prepatfile.FileName, Length = prepatfile.ContentLength };
                }

                _releaseRepository.SaveReleaseDetails(release);

                return RedirectToAction("Index", "Release");
            }
            catch (Exception)
            {
                return View("CreateRelease");
            }
        }

        private static bool HasPrePatFile(HttpPostedFileBase prepatfile)
        {
            return prepatfile!=null  && prepatfile.ContentLength > 0;
        }

        [HttpGet]
        public ViewResult ReleasesOfLastThreeMonths()
        {
            return View("Release", _releaseRepository.GetReleasesOfLastThreeMonths());
        }
    }
}