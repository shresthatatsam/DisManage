using DataAccess.Service.Interface;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace dmSyatem.Controllers
{
    public class PrintServiceController : Controller
    {
        private readonly IPrint _print;
        private readonly IDashboard _dashboard;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PrintServiceController(IPrint print, IHttpContextAccessor httpContextAccessor, IDashboard dashboard)
        {
            _print = print;  
            _httpContextAccessor = httpContextAccessor;
            _dashboard = dashboard;
        }

        public async Task<IActionResult> Index()
        {
           var victimIdString = Guid.Parse(_httpContextAccessor.HttpContext?.Session?.GetString("VictimId"));
            //var victimid = Guid.Parse("D468FEA0-6521-468D-27BC-08DCEC479791");

            //Victim victim =  _print.GetVictimById(victimIdString);
            Victim victim = _dashboard.GetById(victimIdString);
            return View(victim);
        }


    }
}
