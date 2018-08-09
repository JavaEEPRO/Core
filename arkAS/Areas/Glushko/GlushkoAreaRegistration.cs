using System.Web.Mvc;
using System.Web.Optimization;

namespace arkAS.Areas.Glushko
{
    public class GlushkoAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Glushko";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Glushko_default",
                "Glushko/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}