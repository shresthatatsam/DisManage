using DataAccess.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace dmSyatem.Controllers
{
    public class LocationController : Controller
    {
        private readonly ILocation _Location;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LocationController(ILocation Location, IHttpContextAccessor httpContextAccessor)
        {
            _Location = Location;   
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Edit()
        {
            var VictimId = Guid.Parse(_httpContextAccessor?.HttpContext?.Session?.GetString("VictimId"));
            Location data = _Location.getData(VictimId);
            return View(data);
        }


        [HttpPost]
        public async Task<IActionResult> Create(Location location)
        {

            location.VictimId = Guid.Parse(_httpContextAccessor?.HttpContext?.Session?.GetString("VictimId"));
            if (location.VictimId != Guid.Empty)
            {
                var createOrUpdateDisaster = _Location.Create(location);
                _httpContextAccessor.HttpContext?.Session.SetString("LocationId", createOrUpdateDisaster.Id.ToString());
                return RedirectToAction("Index", "Image");
            }
            return View(location);
        }
    }
}
