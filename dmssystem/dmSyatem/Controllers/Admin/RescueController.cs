using DataAccess.Data;
using DataAccess.Service.AdminInterface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace dmSyatem.Controllers.Admin
{
    public class RescueController : Controller
    {
    
        private readonly IRescueTeamService _rescueTeamService;
        public readonly ApplicationDbContext _context;

   

        public RescueController(IRescueTeamService rescueTeamService, ApplicationDbContext context)
        {
            _rescueTeamService = rescueTeamService;

            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.teams = _context.rescueTeams.Include(t => t.province).ToList(); 
            return View();
        }

        public IActionResult Create(Guid? provinceId = null)
          {
            ViewBag.AvailableVolunteers = _context.volunteers.ToList();
            ViewBag.location = _context.provinces.ToList();

        //    if(provinceId.HasValue)
        //{
        //        // Load volunteers based on the selected province
        //        ViewBag.AvailableVolunteers = _context.volunteers
        //            .Where(v => v.provinceId == provinceId.Value) // Adjust according to your data model
        //            .ToList();
        //        //.provinceId = provinceId.Value; // Set the selected province in the model
        //    }
        //else
        //    {
        //        ViewBag.AvailableVolunteers = new List<Volunteer>(); // No volunteers if no province is selected
        //    }

            return View();
        }

        [HttpPost]
        public IActionResult Create(RescueTeam team, List<Guid> SelectedVolunteerIds)
        {
           
                // Add the selected volunteers to the team
                foreach (var volunteerId in SelectedVolunteerIds)
                {
                    var volunteer = _context.volunteers.Find(volunteerId);
                    if (volunteer != null)
                    {
                        team.Volunteers.Add(volunteer);
                    }
                }

                _context.rescueTeams.Add(team);
                _context.SaveChanges();
                return RedirectToAction("Index");
            

            ////ViewBag.Locations = _context.Locations.ToList(); // Repopulate locations if the model state is invalid
            //return View();
        }

     
    }
}

