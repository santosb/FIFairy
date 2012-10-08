using System;
using System.Web.Mvc;
using FIfairyDomain;

namespace FIfairy.Controllers
{
    public class ReleaseController: Controller
    {
        private readonly IReleaseRepository _releaseRepository;

        public ReleaseController(IReleaseRepository releaseRepository)
        {
            _releaseRepository = releaseRepository;            
        }

        [HttpGet]
        public ViewResult Index()
        {
            return View("ReleaseView", _releaseRepository.GetReleases());
        }

        [HttpGet]
        public ViewResult Indexes(DateTime dateFrom, DateTime dateTo)
        {
            return View("ReleaseView",  _releaseRepository.GetReleases(dateFrom, dateTo));
        }
    }
}