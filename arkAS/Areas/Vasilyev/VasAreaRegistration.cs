using System.Web.Mvc;
using System.Web.Optimization;

namespace arkAS.Areas.Vasilyev
{
    public class VasAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Vasilyev";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Vasilyev_default",
                "Vasilyev/{controller}/{action}/{id}",
                new { controller = "Document", action = "Documents", id = UrlParameter.Optional },
                new[] { "arkAS.Areas.Vasilyev.Controllers" }
            );
        }
    }
}