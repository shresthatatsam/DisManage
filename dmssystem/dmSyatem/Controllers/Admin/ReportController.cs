using System.Linq;
using ClosedXML.Excel;
using DataAccess.Data;
using DataAccess.Migrations;
using DataAccess.Service.Interface;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace dmSyatem.Controllers.Admin
{
    public class ReportController : Controller
    {
        public readonly ApplicationDbContext _context;
        private readonly ILocation _Location;

        public ReportController(ILocation Location, ApplicationDbContext context)
        {
            _Location = Location;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.Province = _Location.getProvince();
            return View();
        }
        public IActionResult GenerateDisasterReport()
        {
            return View();
        }

        public IActionResult GenerateDonationReport()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GenerateDonationReport(string donation)
        {
            var query = _context.donations.Where(x => x.Type == donation)
                    .AsQueryable();

            var reports = await query.ToListAsync();
            return View(reports);

        }

        [HttpGet]
        public async Task<IActionResult> JinsiDetails(Guid id)
        {
            var jinsiDonations = await _context.jinsiDonations
                                               .Where(x => x.DonationId == id)
                                               .ToListAsync();

            return Json(jinsiDonations);
        }


        [HttpPost]
        public async Task<IActionResult> GenerateDisasterReport(string disasterType, DateTime? startDate, DateTime? endDate, string phoneNumber, string PermanentProvince, string PermanentDistrict, string PermanentMunicipality, string donation)
        {
            var query = _context.Victims
     .Include(v => v.Disaster)
     .AsQueryable();

            if (!string.IsNullOrEmpty(PermanentProvince))
            {
                var locationIds = _context.Locations
                    .Where(x => x.PermanentProvince == PermanentProvince)
                    .Select(x => x.Id)
                    .ToList();
                query = query.Where(x => locationIds.Contains(x.LocationId));
            }

            if (!string.IsNullOrEmpty(PermanentDistrict))
            {
                var locationIds = _context.Locations
                   .Where(x => x.PermanentDistrict == PermanentDistrict)
                   .Select(x => x.Id)
                   .ToList();
                query = query.Where(x => locationIds.Contains(x.LocationId));
            }

            if (!string.IsNullOrEmpty(PermanentMunicipality))
            {
                var locationIds = _context.Locations
                   .Where(x => x.PermanentMunicipality == PermanentMunicipality)
                   .Select(x => x.Id)
                   .ToList();
                query = query.Where(x => locationIds.Contains(x.LocationId));
            }


            var result = query.ToList();
            if (!string.IsNullOrEmpty(phoneNumber))
            {
                query = query.Where(r => r.ContactNumber == phoneNumber);
            }

            if (!string.IsNullOrEmpty(disasterType) && disasterType != "All")
            {
                var disasterTypes = _context.disasterTypes.Where(x => x.DisasterName == disasterType).Select(x => x.Id).FirstOrDefault().ToString();
                query = query.Where(r => r.Disaster.DisasterType == disasterTypes);
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
                query = query.Where(r => r.Disaster.DateReported >= startDate.Value && r.Disaster.DateReported <= endDate.Value);
            }

            if (!string.IsNullOrEmpty(donation))
            {
                var listDonation = _context.donations.Where(x => x.Type == donation).Select(x => x.VictimId).ToList();
                query = query.Where(x => listDonation.Contains(x.Id));
            }

            var reports = await query.ToListAsync();
            // Return the reports or use them to generate a file
            return View(reports); // You can create a view to display the reports
        }


        //using ClosedXML.Excel; // Add this if you're using ClosedXML

        public IActionResult DownloadExcel()
        {
            var victims = _context.Victims.Include(v=>v.Location).Include(v => v.Disaster).ToList(); // Adjust according to your data fetching logic

            

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Victims");

                // Add headers
                worksheet.Cell(1, 1).Value = "Name";
                worksheet.Cell(1, 2).Value = "Age";
                worksheet.Cell(1, 3).Value = "Gender";
                worksheet.Cell(1, 4).Value = "Contact NUmber";
                worksheet.Cell(1, 5).Value = "Email";
                worksheet.Cell(1, 6).Value = "SecretNumber";
                worksheet.Cell(1, 7).Value = "DisasterName";
                worksheet.Cell(1, 8).Value = "Date";

                // Add data
                for (int i = 0; i < victims.Count; i++)
                {
                    var disasterType = _context.disasterTypes.Where(x => x.Id == victims[i].DisasterId).FirstOrDefault();
                    //var Provincelocation = _context.provinces.Where(x => x.Id == victims[i].Location.PermanentProvince).FirstOrDefault();
                    worksheet.Cell(i + 2, 1).Value = victims[i].Name;
                    worksheet.Cell(i + 2, 2).Value = victims[i].Age;
                    worksheet.Cell(i + 2, 3).Value = victims[i].Gender;
                    worksheet.Cell(i + 2, 4).Value = victims[i].ContactNumber;
                    worksheet.Cell(i + 2, 5).Value = victims[i].Email;
                    worksheet.Cell(i + 2, 6).Value = victims[i].Secret_Number;
                    worksheet.Cell(i + 2, 7).Value = disasterType?.DisasterName ?? "No data"; 
                    worksheet.Cell(i + 2, 8).Value = victims[i].Disaster?.DateReported;
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
