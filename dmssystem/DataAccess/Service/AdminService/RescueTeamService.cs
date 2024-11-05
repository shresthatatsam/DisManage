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
    }
}
