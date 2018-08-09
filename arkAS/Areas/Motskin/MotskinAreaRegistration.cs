using System.Web.Mvc;

namespace arkAS.Areas.Motskin
{
    public class MotskinAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Motskin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Motskin_default",
                "Motskin/{controller}/{action}/{id}",
                new { controller = "Document", action = "Documents", id = UrlParameter.Optional }
            );
        }
    }
}