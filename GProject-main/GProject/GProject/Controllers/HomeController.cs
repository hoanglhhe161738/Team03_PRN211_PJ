using GProject.Libs;
using GProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly NorthwindContext _context = new NorthwindContext();
        [HttpGet]
        public IActionResult getAllProducts(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            List<Product> products = _context.Products.Include(p => p.Category).ToList();
            // Calculate the total number of pages
            int totalProducts = products.Count();
            int totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

            // Get a subset of products based on the current page and page size
            var productsResult = products.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            ViewBag.Products = productsResult;
            ViewBag.TotalPages = totalPages;
            return View("Views/home.cshtml");
        }

        [HttpGet]
        public IActionResult Search(string path, int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            List<Product> products = new List<Product>();

            if (path != null)
            {
                HttpContext.Session.SetString("Path", path);
                products = _context.Products
               .Include(p => p.Category)
               .Where(p => p.ProductName.Contains(path))
               .ToList();
            }
            else
            {
                products = _context.Products
               .Include(p => p.Category)
               .ToList();
            }
            // Calculate the total number of pages
            int totalProducts = products.Count();
            int totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

            // Get a subset of products based on the current page and page size
            var productsResult = products.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            //TempData["SearchResult"] = productsResult;
            ViewBag.Products = productsResult;
            ViewBag.TotalPages = totalPages;
            return View("Views/home.cshtml");
        }

        [HttpPost]
        public IActionResult Sort(string sort, int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            string? path = HttpContext.Session.GetString("Path");
            List<Product> products = new List<Product>();

            if (path != null)
            {
                HttpContext.Session.SetString("Path", path);
                products = _context.Products
               .Include(p => p.Category)
               .Where(p => p.ProductName.Contains(path))
               .ToList();
            }
            else
            {
                products = _context.Products
               .Include(p => p.Category)
               .ToList();
            }
            switch (sort)
            {
                case ActionStrings.SortByNameAZ:
                    products = products.OrderByDescending(p => p.ProductName).ToList();
                    break;
                case ActionStrings.SortByNameZA:
                    products = products.OrderBy(p => p.ProductName).ToList();
                    break;
                case ActionStrings.SortByPriceLowHigh:
                    products = products.OrderBy(p => p.UnitPrice).ToList();
                    break;
                case ActionStrings.SortByPriceHighLow:
                    products = products.OrderByDescending(p => p.UnitPrice).ToList();
                    break;
                default:
                    break;

            }
            int totalProducts = products.Count();
            int totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

            // Get a subset of products based on the current page and page size
            var productsResult = products.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            ViewBag.Products = productsResult;
            ViewBag.TotalPages = totalPages;
            return View("Views/home.cshtml");
        }

        [HttpPost]
        public IActionResult FilterByCategory(int filter, int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            string? path = HttpContext.Session.GetString("Path");
            List<Product> products = new List<Product>();

            if (path != null)
            {
                HttpContext.Session.SetString("Path", path);
                products = _context.Products
               .Include(p => p.Category)
               .Where(p => p.ProductName.Contains(path))
               .ToList();
            }
            else
            {
                products = _context.Products
               .Include(p => p.Category)
               .ToList();
            }
            products = products.Where(p => p.CategoryId == filter).ToList();
            int totalProducts = products.Count();
            int totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

            // Get a subset of products based on the current page and page size
            var productsResult = products.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            ViewBag.Products = productsResult;
            ViewBag.TotalPages = totalPages;
            return View("Views/home.cshtml");
        }

        public IActionResult Index()
        {
            return View("Views/home.cshtml");
        }
    }
}
