using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DataAccess.Service.Interface
{
    public interface ILocation
    {
        Location Create(Location location);
        Location getData(Guid id);
        List<province> getProvince();
        List<province> GetDistricts(Guid provinceId);
        List<province> GetMunicipalities(Guid districtId);
    }
}
