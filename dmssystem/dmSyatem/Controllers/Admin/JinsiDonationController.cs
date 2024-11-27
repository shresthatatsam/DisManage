using DataAccess.Service.AdminInterface;
using DataAccess.Service.AdminService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;

namespace dmSyatem.Controllers.Admin
{
    public class JinsiDonationController : Controller
    {

        private readonly IJinsiService _jinsiService;

        public JinsiDonationController(IJinsiService jinsiService)
        {
            _jinsiService = jinsiService;   
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateJinsiDonation(JinsiDonation donation)
        {
            _jinsiService.Create(donation);
            return View("Index");
        }
        [HttpGet]
        public IActionResult GiveJinsi()
        {
          
            List<JinsiDonation> jinsiDonations = _jinsiService.getAllJinsi(new JinsiDonation());

            
            ViewBag.JinsiDonations = new SelectList(jinsiDonations, "name", "name");

            return View();
        }


    }
}
