using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Data;
using DataAccess.Service.Interface;
using Models;

namespace DataAccess.Service
{
    public class LocationService :ILocation
    {
        private readonly ApplicationDbContext _context;
        public LocationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Location getData(Guid id)
        {
            return _context.Locations.Where(x => x.VictimId == id).FirstOrDefault();

            //var Perprovince = _context.provinces.FirstOrDefault(x => x.Id == Guid.Parse(data.PermanentProvince));
            //var Tempprovince = _context.provinces.Where(x => x.Id == Guid.Parse(data.TemporaryProvince)).FirstOrDefault();
            //var PerDistrict = _context.provinces.Where(x => x.Id == Guid.Parse(data.PermanentDistrict)).FirstOrDefault();
            //var TempDistrict = _context.provinces.Where(x => x.Id == Guid.Parse(data.TemporaryDistrict)).FirstOrDefault();
            //return new Location
            //{
            //    PermanentProvince = Perprovince.Province,
            //    TemporaryProvince = Tempprovince.Province,
            //    PermanentDistrict = PerDistrict.District,
            //    TemporaryDistrict = TempDistrict.District
            //};
        }

        public List<province> getProvince()
        {
            return _context.provinces.Where(x => x.ParentDistrict == Guid.Empty && x.ParentProvince == Guid.Empty).ToList();
        }

        public List<province> GetDistricts(Guid provinceId)
        {
            // Fetch districts based on provinceId
            var districts = _context.provinces
                .Where(d => d.ParentProvince == provinceId && d.ParentDistrict == null) // Ensure you have a relationship defined
                .Select(d => new province
                {
                    Id = d.Id,
                    District = d.District // Adjust as necessary based on your District entity
                })
                .ToList();

            return districts;
        }


        public List<province> GetMunicipalities(Guid districtId)
        {
            return _context.provinces
                .Where(m => m.ParentDistrict == districtId) // Adjust as per your entity structure
                .ToList();
        }


        public Location Create(Location location)
        {

            var existingLocation = _context.Locations
       .FirstOrDefault(d => d.VictimId == location.VictimId);

            if (existingLocation != null)
            {
                existingLocation.PermanentProvince = location.PermanentProvince ?? existingLocation.PermanentProvince;
                existingLocation.PermanentDistrict = location.PermanentDistrict ?? existingLocation.PermanentDistrict;
                existingLocation.PermanentMunicipality = location.PermanentMunicipality ?? existingLocation.PermanentMunicipality;
                existingLocation.PermanentTole = location.PermanentTole ?? existingLocation.PermanentTole;

                existingLocation.TemporaryProvince = location.TemporaryProvince ?? existingLocation.TemporaryProvince;
                existingLocation.TemporaryDistrict = location.TemporaryDistrict ?? existingLocation.TemporaryDistrict;
                existingLocation.TemporaryMunicipality = location.TemporaryMunicipality ?? existingLocation.TemporaryMunicipality;
                existingLocation.TemporaryTole = location.TemporaryTole ?? existingLocation.TemporaryTole;

                _context.SaveChanges();
                if (location.VictimId != Guid.Empty)
                {
                    var victim = _context.Victims.FirstOrDefault(v => v.Id == location.VictimId);
                    if (victim != null)
                    {
                        victim.LocationId = existingLocation.Id;
                        _context.SaveChanges();
                    }
                }
                return existingLocation;
            }
            else
            {

                _context.Locations.Add(location);
                _context.SaveChanges();
                if (location.VictimId != Guid.Empty)
                {
                    var victim = _context.Victims.FirstOrDefault(v => v.Id == location.VictimId);
                    if (victim != null)
                    {
                        victim.LocationId = location.Id;
                        _context.SaveChanges();
                    }
                }
                return location;
            }
        }
    }
}
