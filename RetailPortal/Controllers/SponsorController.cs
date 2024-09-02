using Microsoft.AspNetCore.Mvc;
using RetailPortal.DataAccess;
using RetailPortal.Models;
using System.Collections.Generic;

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
            return View("SponsorIndex",sponsors);
        }

        // GET: Sponsor/Details/5
        public IActionResult Details(int id)
        {
            var sponsor = _repository.GetSponsorDetails().FirstOrDefault(m => m.SponsorId == id);
            if (sponsor == null)
            {
                return NotFound();
            }
            return View(sponsor);
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
                return RedirectToAction(nameof(Index));
            }
            return View("SponsorEdit",sponsor);
        }

        // GET: Sponsor/Edit/5
        public IActionResult Edit(int id)
        {
            var sponsor = _repository.GetSponsorDetails().FirstOrDefault(m => m.SponsorId == id);
            if (sponsor == null)
            {
                return NotFound();
            }
            return View("SponsorEdit",sponsor);
        }

        // POST: Sponsor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, SponsorDetails sponsor)
        {
            if (id != sponsor.SponsorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _repository.UpdateSponsorDetails(sponsor);
                return RedirectToAction(nameof(Index));
            }
            return View("SponsorEdit", sponsor);
        }

        // GET: Sponsor/Delete/5
        public IActionResult Delete(int id)
        {
            var sponsor = _repository.GetSponsorDetails().FirstOrDefault(m => m.SponsorId == id);
            if (sponsor == null)
            {
                return NotFound();
            }
            return View(sponsor);
        }

        // POST: Sponsor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _repository.DeleteSponsorDetails(id);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Next()
        {
            return RedirectToAction("Policy");
        }
    }
}
