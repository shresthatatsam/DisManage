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
            return _context.volunteers.Where(x=>x.isactive).ToList();
        }

        public void AddVolunteer(Volunteer volunteer)
        {
            volunteer.isactive = true;
            _context.volunteers.Add(volunteer);
            _context.SaveChanges();
        }

        public bool EditVolunteer(Volunteer volunteer)
        {
            var existingVolunteer = _context.volunteers.SingleOrDefault(v => v.Id == volunteer.Id);

            if (existingVolunteer == null)
            {
                return false;  // Volunteer not found
            }

            // Update the properties of the existing volunteer
            existingVolunteer.Name = volunteer.Name;
            existingVolunteer.Age = volunteer.Age;
            existingVolunteer.Gender = volunteer.Gender;
            existingVolunteer.ContactNumber = volunteer.ContactNumber;
            existingVolunteer.Email = volunteer.Email;
            existingVolunteer.Availability = volunteer.Availability;
            existingVolunteer.isactive = true;
            existingVolunteer.provinceId = volunteer.provinceId;

            // Optional: Update the province object if it's included
            if (volunteer.province != null)
            {
                existingVolunteer.province = volunteer.province;
            }

            // Save the changes to the database
            _context.SaveChanges();

            return true;  // Return true if the volunteer was updated successfully
        }

        public Volunteer getData(Guid id)
        {
            return _context.volunteers.Where(x => x.Id == id).FirstOrDefault();
        }

        public Guid Delete(Guid id)
        {

            var volunteerData = _context.volunteers.Where(x => x.Id == id).FirstOrDefault();

            volunteerData.isactive = false;

            _context.SaveChanges();
            return id;
        }

    }
}
