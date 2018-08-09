using System.Web.Mvc;

namespace arkAS.Areas.Tsilurik
{
    public class TsilurikAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Tsilurik";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Tsilurik_default",
                "Tsilurik/{controller}/{action}/{id}",
                new { controller = "Document", action = "Documents", id = UrlParameter.Optional }
            );
        }
    }
}