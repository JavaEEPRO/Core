using System.Web.Mvc;

namespace arkAS.Areas.Podvashetskyi
{
    public class PodvashetskyiAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Podvashetskyi";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Podvashetskyi_default",
                "Podvashetskyi/{controller}/{action}/{id}",
                new { controller = "Documents", action = "Documents", id = UrlParameter.Optional },
                new[] { "arkAS.Areas.Podvashetskyi.Controllers" }
            );
        }
    }
}