using System.Web.Mvc;

namespace arkAS.Areas.Mikhailova
{
    public class MikhailovaiAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Mikhailova";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Mikhailova_default",
                "Mikhailova/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}