using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FIfairyDomain;

namespace FIfairy.Controllers
{
    public class ReleaseDetailsController : Controller
    {
        private readonly IReleaseRepository _releaseRepository;

        public ReleaseDetailsController(IReleaseRepository releaseRepository)
        {
            _releaseRepository = releaseRepository;
        }

        public ViewResult Index(string id)
        {
            return View("ReleaseDetails", _releaseRepository.GetReleaseDetails(id));
        }

    }
}
