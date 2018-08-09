using System.Web.Mvc;

namespace arkAS.Areas.Rosh
{
    public class RoshAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Rosh";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Rosh_default",
                "Rosh/{controller}/{action}/{id}",
                new { controller = "Documents", action = "Documents", id = UrlParameter.Optional }
            );
        }
    }
}