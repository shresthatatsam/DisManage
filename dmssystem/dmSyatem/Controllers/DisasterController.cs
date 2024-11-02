using DataAccess.Service;
using DataAccess.Service.AdminInterface;
using DataAccess.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace dmSyatem.Controllers
{
    public class DisasterController : Controller
    {
        private readonly IDisaster _disaster;
        private readonly IDisasterType _disasterType;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DisasterController(IDisaster disaster, IHttpContextAccessor httpContextAccessor,IDisasterType disasterType)
        {
            _disaster = disaster;
            _disasterType = disasterType;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            ViewBag.disasterType = _disasterType.GetAll();

            return View();
        }

        public IActionResult Edit()
        {
            ViewBag.disasterType = _disasterType.GetAll();
            var VictimId = Guid.Parse(_httpContextAccessor?.HttpContext?.Session?.GetString("VictimId"));
            Disaster data = _disaster.getData(VictimId);
            return View(data);
        }


        [HttpPost]
        public async Task<IActionResult> Create(Disaster disaster)
        {
            ViewBag.disasterType = _disasterType.GetAll();
            disaster.VictimId =Guid.Parse(_httpContextAccessor?.HttpContext?.Session?.GetString("VictimId"));
            if (disaster.VictimId != Guid.Empty)
            {
               var createOrUpdateDisaster = _disaster.Create(disaster);
                _httpContextAccessor.HttpContext?.Session.SetString("DisasterId", createOrUpdateDisaster.Id.ToString());
                return RedirectToAction("Edit", "Location");
            }
            return View(disaster);
        }

    }
}
