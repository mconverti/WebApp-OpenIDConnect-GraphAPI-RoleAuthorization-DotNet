namespace WebAppGraphAPIRoleAuthorization.Controllers
{
    using System.Web.Mvc;
    using WebAppGraphAPIRoleAuthorization.Filters;

    [AzureAdAuthorize(Roles = Settings.AdminRole)]
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            this.ViewBag.Message = "Your administration page.";

            return this.View();
        }
    }
}