using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wayfarer.Mvc.Controllers
{
    public class AdminController : Controller
    {

        [Authorize(Roles = "Administrator")] /*Administrator,Editor,Publisher*/
        public ActionResult Index()
        {
            return View();
        }

    }
}
