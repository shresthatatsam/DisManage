using System.Linq;
using DataAccess.Data;
using DataAccess.Service.AdminInterface;
using DataAccess.Service.AdminService;
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
            ViewBag.teams = _context.rescueTeams.Include(x=>x.Volunteers).Include(t => t.province).Where(x=>x.isactive== true).ToList(); 
            return View();
        }

        public IActionResult Create(Guid? provinceId = null)
          {
            ViewBag.AvailableVolunteers = _context.volunteers.Where(x=>x.isactive == true).ToList();
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
                        team.isactive = true;
                        team.Volunteers.Add(volunteer);
                    }
                    volunteer.isactive = false;
                }

                _context.rescueTeams.Add(team);
                _context.SaveChanges();
                return RedirectToAction("Index");
            

            ////ViewBag.Locations = _context.Locations.ToList(); // Repopulate locations if the model state is invalid
            //return View();
        }


		public IActionResult Edit(Guid id)
		{
            //var selectedvol = _context.rescueTeams.Where(x => x.Id == id).Select(x=>x.se);
			ViewBag.AvailableVolunteers = _context.volunteers.Where(x => x.RescueTeamId == id || x.isactive).ToList();
			//ViewBag.AvailableVolunteers = _context.volunteers.Where(x => x.isactive == true).ToList();
			ViewBag.location = _context.provinces.ToList();

            RescueTeam rescueTeam = _rescueTeamService.GetTeamById(id);
			//ViewBag.SelectedVolunteerIds = rescueTeam.Volunteers.Select(v => v.Id).ToList();
			return View(rescueTeam);
		}




		//[HttpPost]
		//public IActionResult Edit(RescueTeam rescueTeam)
		//{
		//          // _context.rescueTeams.Where(x => x.Id == rescueTeam.Id).Select(x => x.SelectedVolunteerIds).ToList();
		//          var existingRescueTeam = _context.volunteers.Where(x => rescueTeam.Id == x.RescueTeamId).Select(x=>x.Id).ToList();
		//          var ExistingSelectedVolunteer = _context.rescueTeams.Where(x => x.Id == rescueTeam.Id).Select(x => x.SelectedVolunteerIds).ToList();

		//          foreach(var item in existingRescueTeam)
		//          {
		//              if(!rescueTeam.SelectedVolunteerIds.Contains(item)){
		//                  var vol =_context.volunteers.Where(x => x.Id == item).FirstOrDefault();
		//                  vol.isactive = true;
		//                  vol.RescueTeamId = null;
		//                  _context.volunteers.Update(vol);
		//                  _context.SaveChanges();
		//              }
		//          }

		//	foreach (var volunteerId in rescueTeam.SelectedVolunteerIds)
		//	{
		//		var volunteer = _context.volunteers.Find(volunteerId);

		//		if (volunteer != null)
		//		{

		//			rescueTeam.isactive = true;
		//			rescueTeam.Volunteers.Add(volunteer);
		//		}
		//		volunteer.isactive = false;
		//	}

		//          _context.rescueTeams.Update(rescueTeam);

		//	_context.SaveChanges();


		//	//_rescueTeamService.EditRescueTeam(rescueTeam);
		//	return RedirectToAction("Index");

		//}

		[HttpPost]
		public IActionResult Edit(RescueTeam rescueTeam)
		{
			// Fetch the existing volunteers assigned to the Rescue Team
			var existingVolunteerIds = _context.volunteers
											   .Where(v => v.RescueTeamId == rescueTeam.Id)
											   .Select(v => v.Id)
											   .ToList();

			// 1. Mark volunteers as inactive and remove their RescueTeamId if they're no longer assigned
			var volunteersToDeactivate = _context.volunteers
												 .Where(v => existingVolunteerIds.Contains(v.Id) && !rescueTeam.SelectedVolunteerIds.Contains(v.Id))
												 .ToList();

			foreach (var volunteer in volunteersToDeactivate)
			{
				volunteer.isactive = true;  // Set them as active again (because they're no longer assigned)
				volunteer.RescueTeamId = null; // Remove them from the rescue team
			}

			// 2. Add new selected volunteers to the rescue team and mark them as active
			var volunteersToAdd = _context.volunteers
										  .Where(v => rescueTeam.SelectedVolunteerIds.Contains(v.Id) && !existingVolunteerIds.Contains(v.Id))
										  .ToList();

			foreach (var volunteer in volunteersToAdd)
			{
				volunteer.isactive = false; // Mark them as inactive since they are now assigned to a rescue team
				rescueTeam.Volunteers.Add(volunteer); // Add them to the rescue team
			}

			// Ensure the rescue team itself is active
			rescueTeam.isactive = true;

			// 3. Update the RescueTeam record in the database
			_context.rescueTeams.Update(rescueTeam);

			// 4. Save all changes to the database in one transaction
			_context.SaveChanges();

			// Redirect to the Index page after saving the changes
			return RedirectToAction("Index");
		}



		public async Task<IActionResult> Delete(RescueTeam RescueTeam)
		{
			_rescueTeamService.Delete(RescueTeam.Id);
			return RedirectToAction("Index");
		}

	}
}

