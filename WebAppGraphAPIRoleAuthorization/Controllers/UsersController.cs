namespace WebAppGraphAPIRoleAuthorization.Controllers
{
    using System.Web.Mvc;
    using WebAppGraphAPIRoleAuthorization.Filters;

    [AzureAdAuthorize(Roles = Settings.UsersRole)]
    public class UsersController : Controller
    {
        public ActionResult Index()
        {
            this.ViewBag.Message = "Your general users' page.";

            return this.View();
        }
    }
}