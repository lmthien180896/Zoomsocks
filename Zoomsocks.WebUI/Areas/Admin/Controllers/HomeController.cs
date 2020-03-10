using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Zoomsocks.WebUI.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Admin()
        {
            return View();
        }
    }
}