using GProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace GProject.Controllers
{
    public class TrendingController : Controller
    {
        private readonly NorthwindContext _context = new NorthwindContext();

        [HttpGet]
        public IActionResult TrendingProduct()
        {
            var topProducts = _context.Products.OrderByDescending(p => p.UnitsOnOrder).Take(10).ToList();
            return View("Views/Trending.cshtml", topProducts);
        }
    }
}
