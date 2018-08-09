using System.Web.Mvc;

namespace arkAS.Areas.Udovika
{
    public class UdovikaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Udovika";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Udovika_default",
                "Udovika/{controller}/{action}/{id}",
                new { controller = "Documents", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}