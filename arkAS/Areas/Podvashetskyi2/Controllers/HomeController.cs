﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using arkAS.Areas.Podvashetskyi.BLL;

namespace arkAS.Areas.Podvashetskyi2.Controllers
{
    public class HomeController : Controller
    {
        // GET: Podvashetskyi/Home
        public ActionResult Index()
        {
            
            return View();
        }
    }
}