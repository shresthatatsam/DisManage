using DataAccess.Data;
using DataAccess.Service.AdminInterface;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace dmSyatem.Controllers.Admin
{
    public class DonationController : Controller
    {
        private readonly IDonationService _donationService;
        private readonly IJinsiService _jinsiService;
        public DonationController(IDonationService donationService, IJinsiService jinsiService)
        {
            _donationService = donationService;
              _jinsiService = jinsiService;
        }
        public IActionResult Index()
        {
            List<JinsiDonation> jinsiDonations = _jinsiService.getAllJinsi(new JinsiDonation());
            ViewBag.JinsiDonations = jinsiDonations;
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

        public IActionResult CreateGiveDonation(Donation donation ,List<JinsiDonation> jinsiDonation)
        {
            _donationService.Create(donation, jinsiDonation);
             return RedirectToAction("Index");
        }
    }
}
