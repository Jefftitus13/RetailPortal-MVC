using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RetailPortal.DataAccess;
using RetailPortal.Models;
using System.Linq;

namespace RetailPortal.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductDetailsRepository _repository;

        public ProductController(ProductDetailsRepository repository)
        {
            _repository = repository;
        }

        // GET: Product/Select
        public IActionResult Select()
        {
            var products = _repository.GetAllProductDetails();
            return View(products);
        }

        // POST: Product/SaveSelectedProduct
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveSelectedProduct(int productId)
        {
            var productDetails = _repository.GetProductDetailsById(productId);

            if (productDetails == null)
            {
                return NotFound();
            }

            // Store the selected product details in TempData
            TempData["ProductDetails"] = JsonConvert.SerializeObject(productDetails);
            return RedirectToAction("Summary");
        }

        // GET: Product/Summary
        public IActionResult Summary()
        {
            // Retrieve the selected product details from TempData
            var productDetailsJson = TempData["ProductDetails"] as string;
            if (productDetailsJson != null)
            {
                var productDetails = JsonConvert.DeserializeObject<ProductDetails>(productDetailsJson);
                return View("Summary", productDetails);
            }

            return RedirectToAction("Select");
        }

        // POST: Product/ProceedToPayment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProceedToPayment()
        {
            return RedirectToAction("Payment");
        }
    }
}
