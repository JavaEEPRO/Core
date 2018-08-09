using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using arkAS.Areas.Molchanov.BLL;

namespace arkAS.Areas.Molchanov.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IManager mng) : base(mng)
        {
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}