﻿using System;
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
        public bool isactive { get; set; }
        public Guid? provinceId { get; set; }
        public province? province { get; set; }

        public Guid? RescueTeamId { get; set; }
		public RescueTeam? rescueTeam { get; set; }

	}
}
