using Microsoft.AspNetCore.Mvc;
using RetailPortal.DataAccess;
using RetailPortal.Models;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace RetailPortal.Controllers
{
    public class SponsorController : Controller
    {
        private readonly SponsorDetailsRepository _repository;

        public SponsorController(SponsorDetailsRepository repository)
        {
            _repository = repository;
        }

        // GET: Sponsor
        public IActionResult Index()
        {
            var sponsors = _repository.GetSponsorDetails().ToList();
            return View("SponsorIndex", sponsors);
        }

        // GET: Sponsor/Create
        public IActionResult Create()
        {
            return View("SponsorCreate");
        }

        // POST: Sponsor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SponsorDetails sponsor)
        {
            if (ModelState.IsValid)
            {
                _repository.AddSponsorDetails(sponsor);
            }
            return View("SponsorCreate");
        }
        
        [HttpPost]
        public IActionResult Next()
        {
            return RedirectToAction("Index", "Policy");
        }

    }

}
