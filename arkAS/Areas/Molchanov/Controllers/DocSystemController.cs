using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using arkAS.Areas.Molchanov.BLL;

namespace arkAS.Areas.Molchanov.Controllers
{
    public class DocSystemController : BaseController
    {
        public DocSystemController(IManager mng) : base(mng)
        {

        }

        // GET: Molchanov/DocSystem
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Tutorial()
        {
            return View();
        }

        public ActionResult Faq()
        {
            return View();
        }

    }
}