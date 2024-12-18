using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Data;
using DataAccess.Service.AdminInterface;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess.Service.AdminService
{
    public class RescueTeamService :IRescueTeamService
    {
        public readonly ApplicationDbContext _context;

        public RescueTeamService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AssignVolunteerToTeam(Guid teamId, Guid volunteerId)
        {
            var team = _context.rescueTeams.Include(t => t.Volunteers).FirstOrDefault(t => t.Id == teamId);
            var volunteer = _context.volunteers.Find(volunteerId);

            if (team != null && volunteer != null)
            {
                team.Volunteers.Add(volunteer);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Volunteer> GetAvailableVolunteers()
        {
            return _context.volunteers.Where(v => v.provinceId == null).ToList(); // Assuming available means not assigned
        }

        public RescueTeam GetTeamById(Guid teamId) 
        {
            return _context.rescueTeams.Include(t => t.Volunteers).FirstOrDefault(t => t.Id == teamId);
        }


		public bool EditRescueTeam(RescueTeam RescueTeam)
		{
			var existingRescueTeam = _context.rescueTeams.FirstOrDefault(v => v.Id == RescueTeam.Id);

			if (existingRescueTeam == null)
			{
				return false;  // Volunteer not found
			}

			// Update the properties of the existing volunteer
			existingRescueTeam.TeamName = RescueTeam.TeamName;
			existingRescueTeam.isactive = true;
			existingRescueTeam.SelectedVolunteerIds = RescueTeam.SelectedVolunteerIds;
			existingRescueTeam.provinceId = RescueTeam.provinceId;
			

			// Optional: Update the province object if it's included
			if (existingRescueTeam.province != null)
			{
				existingRescueTeam.province = RescueTeam.province;
			}


		

		// Save the changes to the database
		_context.SaveChanges();

			return true;  // Return true if the volunteer was updated successfully
		}



		public Guid Delete(Guid id)
		{

			var rescueData = _context.rescueTeams.Where(x => x.Id == id).FirstOrDefault();

			rescueData.isactive = false;

			_context.SaveChanges();

			if (rescueData != null)
			{
				
				foreach (var item in rescueData.SelectedVolunteerIds)
				{
                  var volunteerData=  _context.volunteers.Where(x => x.Id == item).FirstOrDefault();
                    volunteerData.isactive =true;
				}

				_context.SaveChanges();
			}

			return id;
		}

	}
}
