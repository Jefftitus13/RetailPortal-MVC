using Microsoft.AspNetCore.Mvc;
using RetailPortal.DataAccess;
using RetailPortal.Models;
using System.Linq;

namespace RetailPortal.Controllers
{
    public class MemberController : Controller
    {
        private readonly MemberDetailsRepository _repository;

        public MemberController(MemberDetailsRepository repository)
        {
            _repository = repository;
        }

        // GET: Member
        public IActionResult Index()
        {
            var members = _repository.GetMemberDetails().ToList();
            return View("MemberIndex",members);
        }

        // GET: Member/Details/5
        public IActionResult Details(int id)
        {
            var member = _repository.GetMemberDetails().FirstOrDefault(m => m.MemberId == id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // GET: Member/Create
        public IActionResult Create()
        {
            return View("MemberCreate");
        }

        // POST: Member/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MemberDetails member)
        {
            if (ModelState.IsValid)
            {
                _repository.AddMemberDetails(member);
                return RedirectToAction("ProductDetails");
            }
            return View(member);
        }

        // GET: Member/Edit/5
        public IActionResult Edit(int id)
        {
            var member = _repository.GetMemberDetails().FirstOrDefault(m => m.MemberId == id);
            if (member == null)
            {
                return NotFound();
            }
            return View("MemberEdit",member);
        }

        // POST: Member/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, MemberDetails member)
        {
            if (id != member.MemberId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _repository.UpdateMemberDetails(member);
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        // GET: Member/Delete/5
        public IActionResult Delete(int id)
        {
            var member = _repository.GetMemberDetails().FirstOrDefault(m => m.MemberId == id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: Member/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _repository.DeleteMemberDetails(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
