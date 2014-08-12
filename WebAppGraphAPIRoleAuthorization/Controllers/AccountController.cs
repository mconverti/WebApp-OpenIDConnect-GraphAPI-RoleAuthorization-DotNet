namespace WebAppGraphAPIRoleAuthorization.Controllers
{
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.Owin.Security.Cookies;
    using Microsoft.Owin.Security.OpenIdConnect;

    [Authorize]
    public class AccountController : Controller
    {
        public void SignOut()
        {
            // Send an OpenID Connect sign-out request.
            this.HttpContext.GetOwinContext().Authentication.SignOut(
                OpenIdConnectAuthenticationDefaults.AuthenticationType, CookieAuthenticationDefaults.AuthenticationType);
        }

        public ActionResult Unauthorized()
        {
            return this.View();

            this.ViewBag.Message = "You are not authorized to accesss this section";
        }
    }
}