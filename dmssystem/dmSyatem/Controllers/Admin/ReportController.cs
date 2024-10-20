using ClosedXML.Excel;
using DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

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
        public async Task<IActionResult> GenerateDisasterReport(string disasterType, DateTime? startDate, DateTime? endDate ,string phoneNumber)
        {
            var query = _context.Victims
            .Include(v => v.Disaster) 
                .AsQueryable();

            if (!string.IsNullOrEmpty(phoneNumber))
            {
                query = query.Where(r => r.ContactNumber == phoneNumber);
            }

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

            if (startDate.HasValue && endDate.HasValue)
            {
                query = query.Where(r => r.Disaster.DateReported >= startDate.Value && r.Disaster.DateReported <= endDate.Value );
            }


            var reports = await query.ToListAsync();
            // Return the reports or use them to generate a file
            return View(reports); // You can create a view to display the reports
        }


        //using ClosedXML.Excel; // Add this if you're using ClosedXML

public IActionResult DownloadExcel()
    {
        var victims = _context.Victims.Include(v => v.Disaster).ToList(); // Adjust according to your data fetching logic

        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Victims");

            // Add headers
            worksheet.Cell(1, 1).Value = "Name";
            worksheet.Cell(1, 2).Value = "Phone Number";
            worksheet.Cell(1, 3).Value = "Disaster Type";
            worksheet.Cell(1, 4).Value = "Date";

            // Add data
            for (int i = 0; i < victims.Count; i++)
            {
                worksheet.Cell(i + 2, 1).Value = victims[i].Name;
                worksheet.Cell(i + 2, 2).Value = victims[i].ContactNumber;
                worksheet.Cell(i + 2, 3).Value = victims[i].Disaster?.DisasterType;
                worksheet.Cell(i + 2, 4).Value = victims[i].Disaster?.DateReported;
            }

            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                var fileName = "DisasterReports.xlsx";
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }
    }


}
}
