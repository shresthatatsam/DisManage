using DataAccess.Data;
using DataAccess.Service.AdminInterface;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace dmSyatem.Controllers.Admin
{
    public class DonationController : Controller
    {
        private readonly IDonationService _donationService;

        public DonationController(IDonationService donationService)
        {
            _donationService = donationService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ReceiveDonation()
        {
            return View();
        }

        public IActionResult GiveDonation()
        {
            return View();
        }

        public IActionResult CreateGiveDonation(Donation donation)
        {
            _donationService.Create(donation);
             return View("Index");
        }
    }
}
