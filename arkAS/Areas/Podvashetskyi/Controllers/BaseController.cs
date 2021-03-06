﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using arkAS.Areas.Podvashetskyi.BLL;

namespace arkAS.Areas.Podvashetskyi.Controllers
{
    public class BaseController : Controller
    {
        protected IManager mng;
        public BaseController(IManager mng)
        {
            this.mng = mng;
        }
        protected override void Dispose(bool disposing)
        {
            if (mng != null)
                mng.Dispose();
        }
    }
}