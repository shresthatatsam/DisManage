using DataAccess.Service.AdminInterface;
using DataAccess.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace dmSyatem.Controllers.Admin
{
	public class VictimManagementController : Controller
	{
		public readonly IVictimManagement _victim;
		public VictimManagementController(IVictimManagement victim)
		{
			_victim = victim;
		}

		public IActionResult Index()
		{
			List<Victim> victimViewModel = _victim.recentDisaster();
			return View(victimViewModel);
		}
	}
}
