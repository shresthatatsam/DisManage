using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Data;
using DataAccess.Service.AdminInterface;
using Models;

namespace DataAccess.Service.AdminService
{
    public class VolunteerService :IVolunteerService
    {
        private readonly ApplicationDbContext _context;

        public VolunteerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Volunteer> GetAllVolunteers()
        {
            return _context.volunteers.ToList();
        }

        public void AddVolunteer(Volunteer volunteer)
        {
            _context.volunteers.Add(volunteer);
            _context.SaveChanges();
        }
    }
}
