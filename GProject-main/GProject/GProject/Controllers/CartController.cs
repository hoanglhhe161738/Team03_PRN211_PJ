using GProject.Libs;
using GProject.Models;
using Microsoft.AspNetCore.Mvc;

public class CartController : Controller
{
    private readonly NorthwindContext _context;

    public CartController(NorthwindContext context)
    {
        _context = context;
    }
    [HttpGet]
    public IActionResult AddToCart(int productId)
    {
        var product = _context.Products.FirstOrDefault(p => p.ProductId == productId);

        if (product != null)
        {
            var cart = HttpContext.Session.GetObject<List<CartItem>>("Cart") ?? new List<CartItem>();

            var existingItem = cart.FirstOrDefault(item => item.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = productId,
                    ProductName = product.ProductName,
                    UnitPrice = product.UnitPrice,
                    Quantity = 1
                });
            }

            // Lưu giỏ hàng vào Session
            HttpContext.Session.SetObject("Cart", cart);
        }

        return RedirectToAction("getAllProducts", "Home");
    }

    public IActionResult ViewCart()
    {
        // Lấy giỏ hàng từ Session
        var cart = HttpContext.Session.GetObject<List<CartItem>>("Cart") ?? new List<CartItem>();
        return View("Views/ViewCart.cshtml", cart);
    }
}
