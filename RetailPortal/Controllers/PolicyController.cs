using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RetailPortal.Models;
using System.Linq;
using Newtonsoft.Json;

namespace RetailPortal.Controllers
{
    public class PolicyController : Controller
{
    private readonly PolicyDetailsRepository _repository;

    public PolicyController(PolicyDetailsRepository repository)
    {
        _repository = repository;
    }

    // GET: Policy/Select
    public IActionResult Select()
    {
        var policies = _repository.GetAllPolicyDetails();
        return View(policies);
    }

    // POST: Policy/SaveSelectedPolicy
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult SaveSelectedPolicy(int policyId)
    {
        var policyDetails = _repository.GetPolicyDetails(policyId);

        if (policyDetails == null)
        {
            return NotFound();
        }

        //policy details in TempData
        TempData["PolicyDetails"] = JsonConvert.SerializeObject(policyDetails);
        return RedirectToAction("Summary");
    }

    // GET: Policy/Summary
    public IActionResult Summary()
    {
        // Retrieve the selected policy details from TempData
        var policyDetailsJson = TempData["PolicyDetails"] as string;
        if (policyDetailsJson != null)
        {
            var policyDetails = JsonConvert.DeserializeObject<PolicyDetails>(policyDetailsJson);
            return View("Summary", policyDetails);
        }

        return RedirectToAction("Select");
    }

    // POST: Policy/ProceedToMemberDetails
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ProceedToMemberDetails()
    {
        return RedirectToAction("MemberDetails");
    }
}
}
