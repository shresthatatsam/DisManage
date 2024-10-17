using DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dmSyatem.Controllers.Admin
{
    public class ReportController : Controller
    {
        public readonly ApplicationDbContext _context;

        public ReportController(ApplicationDbContext context)
        {
            _context = context;
        }
      
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GenerateDisasterReport()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GenerateDisasterReport(string disasterType, DateTime? startDate, DateTime? endDate)
        {
            var query = _context.Victims.AsQueryable();

            if (!string.IsNullOrEmpty(disasterType) && disasterType != "All")
            {
                query = query.Where(r => r.Disaster.DisasterType == disasterType);
            }

            if (startDate.HasValue)
            {
                query = query.Where(r => r.Disaster.DateReported >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(r => r.Disaster.DateReported <= endDate.Value);
            }

            var reports = await query.ToListAsync();
            // Return the reports or use them to generate a file
            return View(reports); // You can create a view to display the reports
        }
    }
}
