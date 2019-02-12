using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OpenCaseManager.Controllers
{
    [Authorize]
    public class DigitalAssistantController : Controller
    {
        // GET: DigitalAssistant
        public ActionResult Index()
        {
            return View();
        }

        // GET: DigitalAssistant
        public ActionResult Details(int caseId)
        {
            return View();
        }
    }
}