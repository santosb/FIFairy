using System;
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
        public ActionResult Create(ReleaseDetailsModel releaseDetailsModel)
        {
            try
            {
                _releaseRepository.SaveReleaseDetails(releaseDetailsModel);
                              
                return RedirectToAction("Index", "Release");
            }
            catch
            {
                return View("CreateRelease");
            }
        }
    }
}