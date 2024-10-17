using DataAccess.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace dmSyatem.Controllers.Admin
{
    public class DashboardController : Controller
    {

        public readonly IVictimService _victim;
        private readonly IDashboard _dashboard;
        public DashboardController(IVictimService victim, IDashboard dashboard)
        {
            _dashboard = dashboard;
            _victim = victim;
        }

 

        public async Task<IActionResult> Index()
        {
            ViewBag.VictimCount = _dashboard.TotalVictim();
            ViewBag.ActiveCases = _dashboard.ActiveCases();
            ViewBag.ResourceDeployed = _dashboard.ResourceDeployed();
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
