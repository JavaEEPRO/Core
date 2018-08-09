using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using arkAS.Areas.Udovika.BLL;
using arkAS.BLL;

namespace arkAS.Areas.Udovika.Controllers
{
    public class BaseController : Controller
    {
        protected IManager mng;
        public BaseController(IManager mng)
        {
            this.mng = mng;
        }
    }
}