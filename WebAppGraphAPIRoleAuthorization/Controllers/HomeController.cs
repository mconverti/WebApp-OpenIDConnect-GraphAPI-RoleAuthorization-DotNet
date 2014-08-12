namespace WebAppGraphAPIRoleAuthorization.Controllers
{
    using System.Web.Mvc;

    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            this.ViewBag.Message = "Your home page.";

            return this.View();
        }
    }
}