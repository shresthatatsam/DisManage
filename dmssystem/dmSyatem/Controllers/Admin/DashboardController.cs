using DataAccess.Service;
using DataAccess.Service.AdminInterface;
using DataAccess.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace dmSyatem.Controllers.Admin
{
    public class DashboardController : Controller
    {

        public readonly IVictimService _victim;
        private readonly IDashboard _dashboard;
        public readonly IDonationService _donationService;
        public DashboardController(IVictimService victim, IDashboard dashboard, IDonationService donationService)
        {
            _dashboard = dashboard;
            _victim = victim;
            _donationService = donationService;
        }

 

        public async Task<IActionResult> Index()
        {
			var disasterCounts = await _dashboard.GetMonthlyDisasterCountsAsync();
			var disasterMonths = Enumerable.Range(1, 6).ToList(); // For example, use last 6 months

        
            ViewBag.DisasterCounts = disasterCounts;
			ViewBag.DisasterMonths = disasterMonths;
			


			ViewBag.VictimCount = _dashboard.TotalVictim();
            ViewBag.ActiveCases = _dashboard.ActiveCases();
            ViewBag.ResourceDeployed = _dashboard.ResourceDeployed();

            ViewBag.donationReceived = _donationService.TotalDonationReceived(); 
            ViewBag.donationGiven = _donationService.TotalDonationGiven();
            ViewBag.TotalDonationReceivedAmount = _donationService.TotalDonationReceivedAmount();
            ViewBag.TotalDonationGivenAmount = _donationService.TotalDonationGivenAmount();

            List<Victim> victimViewModel = _dashboard.recentDisaster();
            return View(victimViewModel);
        }

        public IActionResult Details(Guid id)
        {
			Victim item = _dashboard.GetById(id);
			return View(item);
        }

    }
}
