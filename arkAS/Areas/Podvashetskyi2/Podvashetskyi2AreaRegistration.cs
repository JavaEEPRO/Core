using System.Web.Mvc;

namespace arkAS.Areas.Podvashetskyi
{
    public class Podvashetskyi2AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Podvashetskyi2";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Podvashetskyi2_default",
                "Podvashetskyi2/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "arkAS.Areas.Podvashetskyi2.Controllers" }
            );
        }
    }
}