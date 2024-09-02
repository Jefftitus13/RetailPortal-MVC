using Microsoft.AspNetCore.Mvc;

namespace RetailPortal.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string phone)
        {
            return RedirectToAction("Index", "Sponsor");
        }
    }
}
