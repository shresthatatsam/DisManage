using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class RescueTeam
    {
       
            public Guid Id { get; set; }
            public string TeamName { get; set; }
            public List<Volunteer> Volunteers { get; set; } = new List<Volunteer>();
       

         public Guid provinceId { get; set; }
            public province province { get; set; }

        public List<Guid> SelectedVolunteerIds { get; set; } = new List<Guid>();

    }
}
