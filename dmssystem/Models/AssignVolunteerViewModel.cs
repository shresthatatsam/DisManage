using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class AssignVolunteerViewModel
    {
        public Guid Id { get; set; }
        public string TeamName { get; set; }
        public IEnumerable<Volunteer> AvailableVolunteers { get; set; }
        public Guid SelectedVolunteerId { get; set; }
    }
}
