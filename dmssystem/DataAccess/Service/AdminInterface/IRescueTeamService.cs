﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DataAccess.Service.AdminInterface
{
    public interface IRescueTeamService
    {
        void AssignVolunteerToTeam(Guid teamId, Guid volunteerId);
        IEnumerable<Volunteer> GetAvailableVolunteers();
        bool EditRescueTeam(RescueTeam RescueTeam);
        RescueTeam GetTeamById(Guid teamId);
		Guid Delete(Guid id);
	}
}
