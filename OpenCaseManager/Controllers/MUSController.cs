using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OpenCaseManager.Controllers
{
    [Authorize]
    public class MUSController : Controller
    {
        // GET: MUS
        public ActionResult Index()
        {
            return View();
        }
    }
}