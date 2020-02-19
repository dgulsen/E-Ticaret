using System.Web.Mvc;

namespace AksesuarSat_V1.Areas.AdminPaneli
{
    public class AdminPaneliAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AdminPaneli";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AdminPaneli_default",
                "AdminPaneli/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}