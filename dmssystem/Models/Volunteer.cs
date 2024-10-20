using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Volunteer
    {
        public Guid Id { get; set; }    
        public string Name { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; } 
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string Availability { get; set; }
        public Guid? LocationId { get; set; } 
        public Location? Location { get; set; }

    }
}
