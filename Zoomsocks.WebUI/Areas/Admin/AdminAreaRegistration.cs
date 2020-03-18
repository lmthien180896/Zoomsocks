using System.Web.Mvc;

namespace Zoomsocks.WebUI.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Admin", controller = "Admin", id = UrlParameter.Optional },
                namespaces: new[] { "Zoomsocks.WebUI.Areas.Admin.Controllers" }
            );
        }
    }
}