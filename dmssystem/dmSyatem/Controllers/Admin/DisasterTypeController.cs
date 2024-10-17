using DataAccess.Migrations;
using DataAccess.Service.AdminInterface;
using DataAccess.Service.AdminService;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace dmSyatem.Controllers.Admin
{
    public class DisasterTypeController : Controller
    {
        private readonly IDisasterType _disasterTypeService;

        public DisasterTypeController(IDisasterType disasterTypeService)
        {
            _disasterTypeService = disasterTypeService;
        }
        public IActionResult Index()
        {
            List<DisasterType> disasterTypes = _disasterTypeService.GetAll();
            return View(disasterTypes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DisasterType disasterType)
        {
            if (ModelState.IsValid)
            {
                _disasterTypeService.create(disasterType);
                return RedirectToAction("Index");
            }
            return View(disasterType);
        }

       
    }
}
