using Gp.Services;
using GProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace GProject.Controllers
{
    public class VNPAYController : Controller
    {
        private readonly IVnPayService _vnPayService;

        public VNPAYController(IVnPayService vnPayService)
        {
            _vnPayService = vnPayService;
        }

        [HttpPost(Name = "create-payment")]
        public IActionResult CreatePaymentUrl(string OrderType, double Amount, string OrderDescription, string Name)
        {
            PaymentInformationModel model = new PaymentInformationModel();
            model.OrderType = OrderType;
            model.Amount = Amount * 23000;
            model.OrderDescription = OrderDescription;
            model.Name = Name;
            HttpContext.Session.SetString("Amount", (Amount * 23000).ToString());
            //ViewData["Amount"] = HttpContext.Session.GetString("Amount");
            return Redirect(_vnPayService.CreatePaymentUrl(model, HttpContext));
        }
        [HttpGet]
        public IActionResult PaymentCallback()
        {
            PaymentResponseModel response = (PaymentResponseModel)_vnPayService.PaymentExecute(Request.Query);
            ViewBag.Response = response;
            ViewBag.PaymentInformation = HttpContext.Session.GetString("Amount");
            return View("/Views/Result.cshtml", response);
        }
    }

}
