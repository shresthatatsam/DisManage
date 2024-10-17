﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service.AdminInterface
{
    public interface IDisasterType
    {
        DisasterType create(DisasterType disasterTypeService);
        List<DisasterType> GetAll();
    }
}
