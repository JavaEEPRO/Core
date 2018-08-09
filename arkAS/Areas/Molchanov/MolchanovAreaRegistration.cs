using System.Web.Mvc;

namespace arkAS.Areas.Molchanov
{
    public class MolchanovAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Molchanov";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Molchanov_default",
                "Molchanov/{controller}/{action}/{id}",
                new { controller = "DocSystem", action = "Index", id = UrlParameter.Optional },
                new[] { "arkAS.Areas.Molchanov.Controllers" }
            );
        }
    }
}