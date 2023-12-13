using Microsoft.AspNetCore.Mvc;

namespace GProject.Controllers
{
    public class OrderTrackingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
