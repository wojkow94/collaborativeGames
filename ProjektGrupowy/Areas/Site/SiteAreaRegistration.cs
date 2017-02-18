using System.Web.Mvc;

namespace ProjektGrupowy.Areas.Site
{
    public class SiteAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Site";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                name:  "SiteArea",
                url:  "Site/{controller}/{action}/{id}",
                defaults: new { controller = "HomePage", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
                name: "HomePage",
                url: "",
                defaults: new { area = "Site", controller = "HomePage", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}