using Microsoft.AspNetCore.Mvc;

namespace GProject.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
