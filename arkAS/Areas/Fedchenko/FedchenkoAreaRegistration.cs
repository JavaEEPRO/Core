using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace arkAS.Areas.Fedchenko
{
    public class FedchenkoAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Fedchenko";
            }
        }
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Fedchenko_default",
                "Fedchenko/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
                );
        }
    }
}