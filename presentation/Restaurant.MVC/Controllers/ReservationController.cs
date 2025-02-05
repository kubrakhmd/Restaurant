using Microsoft.AspNetCore.Mvc;

namespace Restaurant.MVC.Controllers
{
    public class ReservationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
