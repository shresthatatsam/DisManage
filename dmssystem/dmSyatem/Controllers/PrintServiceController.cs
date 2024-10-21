using DataAccess.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace dmSyatem.Controllers
{
    public class PrintServiceController : Controller
    {
        private readonly IPrint _print;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PrintServiceController(IPrint print, IHttpContextAccessor httpContextAccessor)
        {
            _print = print;  
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
           var victimIdString = Guid.Parse(_httpContextAccessor.HttpContext?.Session?.GetString("VictimId"));
            //var victimid = Guid.Parse("D468FEA0-6521-468D-27BC-08DCEC479791");

            Victim victim =  _print.GetVictimById(victimIdString);
            return View(victim);
        }


    }
}
