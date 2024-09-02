using Microsoft.AspNetCore.Mvc;
using RetailPortal.Models;
using System.Linq;
using RetailPortal.DataAccess;

namespace RetailPortal.Controllers
{
    public class PaymentController : Controller
    {
        private readonly PaymentRepository _repository;

        public PaymentController(PaymentRepository repository)
        {
            _repository = repository;
        }

        // GET: Payment
        public IActionResult Index()
        {
            var payments = _repository.GetPayments();
            return View(payments);
        }

        // GET: Payment/Details/5
        public IActionResult Details(int id)
        {
            var payment = _repository.GetPaymentById(id);
            if (payment == null)
            {
                return NotFound();
            }
            return View(payment);
        }

        // GET: Payment/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Payment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Payment payment)
        {
            if (ModelState.IsValid)
            {
                _repository.AddPayment(payment);
                return RedirectToAction(nameof(Index));
            }
            return View(payment);
        }
    }
}
