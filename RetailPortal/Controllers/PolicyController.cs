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
            var sponsorDetailsJson = TempData["SponsorDetails"] as string;
            if (sponsorDetailsJson == null)
            {
                return RedirectToAction("Create", "Sponsor");
            }

            var sponsorDetails = JsonConvert.DeserializeObject<SponsorDetails>(sponsorDetailsJson);
            ViewBag.SponsorDetails = sponsorDetails;

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

            // Retrieve SponsorDetails from TempData
            var sponsorDetailsJson = TempData["SponsorDetails"] as string;
            if (sponsorDetailsJson == null)
            {
                return RedirectToAction("Create", "Sponsor");
            }

            var sponsorDetails = JsonConvert.DeserializeObject<SponsorDetails>(sponsorDetailsJson);

            // Save both PolicyDetails and SponsorDetails in TempData
            TempData["PolicyDetails"] = JsonConvert.SerializeObject(policyDetails);
            TempData["SponsorDetails"] = JsonConvert.SerializeObject(sponsorDetails);
            
            return RedirectToAction("Summary");
    }

    // GET: Policy/Summary
    public IActionResult Summary()
    {
        // Retrieve the selected policy details from TempData
        var policyDetailsJson = TempData["PolicyDetails"] as string;
        var sponsorDetailsJson = TempData["SponsorDetails"] as string;

            if (policyDetailsJson == null || sponsorDetailsJson == null)
            {
                return RedirectToAction("Select");
            }

            var policyDetails = JsonConvert.DeserializeObject<PolicyDetails>(policyDetailsJson);
            var sponsorDetails = JsonConvert.DeserializeObject<SponsorDetails>(sponsorDetailsJson);

            var viewModel = new PolicySummaryViewModel
            {
                PolicyDetails = policyDetails,
                SponsorDetails = sponsorDetails
            };

            return View("Summary", viewModel);
        }

    // POST: Policy/ProceedToMemberDetails
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ProceedToMemberDetails()
    {
            var policyDetailsJson = TempData["PolicyDetails"] as string;
            var sponsorDetailsJson = TempData["SponsorDetails"] as string;

            if (policyDetailsJson == null || sponsorDetailsJson == null)
            {
                return RedirectToAction("Select");
            }

            // Optionally, store the details again in TempData if they need to be passed to the next action
            TempData["PolicyDetails"] = policyDetailsJson;
            TempData["SponsorDetails"] = sponsorDetailsJson;

            return RedirectToAction("MemberIndex", "Member");
        }
}
}
