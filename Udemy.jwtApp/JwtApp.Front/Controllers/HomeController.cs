using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtApp.Front.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public string AdminPage()
        {
            return "Admin page";
        }
        [Authorize(Roles = "Member")]
        public string MemberPage()
        {
            return "Member page";
        }
    }
}
