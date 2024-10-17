using DataAccess.Service;
using DataAccess.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace dmSyatem.Controllers
{
    public class DisasterController : Controller
    {
        private readonly IDisaster _disaster;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DisasterController(IDisaster disaster, IHttpContextAccessor httpContextAccessor)
        {
            _disaster = disaster;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Edit()
        {
            var VictimId = Guid.Parse(_httpContextAccessor?.HttpContext?.Session?.GetString("VictimId"));
            Disaster data = _disaster.getData(VictimId);
            return View(data);
        }


        [HttpPost]
        public async Task<IActionResult> Create(Disaster disaster)
        {
         
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
