﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DataAccess.Service.Interface
{
    public interface IDashboard
    {
        string TotalVictim();
        string ActiveCases();
        Task<IEnumerable<int>> GetMonthlyDisasterCountsAsync();
        //Task<IEnumerable<VictimData>> GetVictimDataAsync();


        string ResourceDeployed();
        List<Victim> recentDisaster();
        Victim GetById(Guid id);
    }
}
