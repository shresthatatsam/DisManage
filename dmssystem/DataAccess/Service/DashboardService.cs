using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Data;
using DataAccess.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataAccess.Service
{
    public class DashboardService :IDashboard
    {
        public readonly ApplicationDbContext _context;

               public DashboardService(ApplicationDbContext context)
               {
                   _context = context;
               }

		public async Task<IEnumerable<int>> GetMonthlyDisasterCountsAsync()
		{
			// Example: Count disasters per month over the last 6 months
			var disasterCounts = await _context.Disasters
				.Where(d => d.created_at >= DateTime.Now.AddMonths(-6))
				.GroupBy(d => d.created_at.Month)
				.Select(group => group.Count())
				.ToListAsync();

			return disasterCounts;
		}

       
        private province GetProvince(string id)
        {
            if (string.IsNullOrEmpty(id)) return null;
            return _context.provinces.FirstOrDefault(x => x.Id == Guid.Parse(id));
        }

        public string TotalVictim()
               {
                  var totalVictim = _context.Victims.Count().ToString();
                   return totalVictim;
               }

               public string ActiveCases()
               {
                   var totalVictim = _context.Victims.Where(x=>x.isActive ==true).Count().ToString();
                   return totalVictim;
               }

               public string ResourceDeployed()
               {
                   var totalVictim = _context.Victims.Where(x => x.isActive == false).Count().ToString();
                   return totalVictim;
               }

        public List<Victim> recentDisaster()
        {
            var data = _context.Victims
                .Select(x => new Victim
                {
                    Id = x.Id,
                    Name=x.Name,
                    isActive = x.isActive,
                    created_at = x.created_at,
                    Location = new Location
                    {
                        PermanentDistrict = x.Location.PermanentDistrict
                    }
                })
                .OrderByDescending(x => x.created_at) // Ensure that the records are ordered by 'created_at'
                .Take(10)
                .ToList();

            return data; // Return the data instead of null
        }


        public Victim GetById(Guid id)
        {
           var item = _context.Victims.Where(x => x.Id == id).FirstOrDefault();
            var location = _context.Locations.Where(x => x.Id == item.LocationId).FirstOrDefault();
            var disaster = _context.Disasters.Where(x => x.Id == item.DisasterId).FirstOrDefault();
            var donation = _context.donations.Where(x => x.id == item.DonationId).FirstOrDefault();
           
            List<string> jinsiList = new List<string>();
            if(donation!= null)
            {

            foreach (var jinsiitem in donation.Jinsi)
            {
             
                Guid jinsiId = Guid.Parse(jinsiitem);

                var fetchedJinsi = _context.jinsiDonations
                                           .Where(x => x.id == jinsiId)
                                           .Select(x=>x.name); 

                jinsiList.AddRange(fetchedJinsi); 
            }
            }



            var disasterType = _context.disasterTypes.Where(x => x.Id == Guid.Parse(disaster.DisasterType)).FirstOrDefault();
            var Perprovince = GetProvince(location.PermanentProvince);
            var Tempprovince = GetProvince(location.TemporaryProvince);
            var PerDistrict = GetProvince(location.PermanentDistrict);
            var TempDistrict = GetProvince(location.TemporaryDistrict);
            var PerMun = GetProvince(location.PermanentMunicipality);
            var TempMun = GetProvince(location.TemporaryMunicipality);


            var victim = _context.Victims
        .Where(x => x.Id == id)
        .Select(v => new Victim
        {
            Id = v.Id,
            Name = v.Name,
            Secret_Number = v.Secret_Number,
            Age = v.Age,
            Gender = v.Gender,
            ContactNumber = v.ContactNumber,
            Email = v.Email,
            PPSizePhoto = v.PPSizePhoto,
            CitizenshipNumber = v.CitizenshipNumber,
            NIDNumber = v.NIDNumber,
            FatherName = v.FatherName,
            MotherName = v.MotherName,
            GrandFatherName = v.GrandFatherName,
            isActive = v.isActive,
            created_at = v.created_at,
            Location = v.Location != null ? new Location
            {
                Id = v.Location.Id,
                PermanentProvince = Perprovince != null ? Perprovince.Province : string.Empty,
                PermanentDistrict = PerDistrict != null ? PerDistrict.District : string.Empty,
                PermanentMunicipality = PerMun != null ? PerMun.Municipality : string.Empty,
                PermanentTole = v.Location.PermanentTole,
                TemporaryProvince = Tempprovince != null ? Tempprovince.Province : string.Empty,
                TemporaryDistrict = TempDistrict != null ? TempDistrict.District : string.Empty,
                TemporaryMunicipality = TempMun != null ? TempMun.Municipality : string.Empty,
                TemporaryTole = v.Location.TemporaryTole,
                isActive = v.Location.isActive,
                created_at = v.Location.created_at
            } : null, // Null check in case Location is not present

            Disaster = v.Disaster != null ? new Disaster
            {
                Id = v.Disaster.Id,
                DisasterType = disasterType.DisasterName,
                Description = v.Disaster.Description,
                DateReported = v.Disaster.DateReported,
                created_at = v.Disaster.created_at,
                isActive = v.Disaster.isActive,
                Images = v.Disaster.Images != null ? v.Disaster.Images.Select(i => new Image
                {
                    Id = i.Id,
                    ImageUrl = i.ImageUrl,
                    Description = i.Description,
                    DisasterDate = i.DisasterDate,
                    isActive = i.isActive,
                    created_at = i.created_at
                }).ToList() : new List<Image>()
            } : null, // Null check in case Disaster is not present

            Images = v.Images != null ? v.Images.Select(i => new Image
            {
                Id = i.Id,
                ImageUrl = i.ImageUrl,
                Description = i.Description,
                DisasterDate = i.DisasterDate,
                isActive = i.isActive,
                created_at = i.created_at
            }).ToList() : new List<Image>(), // Return images related to the victim\
            Donation = v.Donation != null ? new Donation
            {
                id = v.Donation.id,
                amount = v.Donation.amount,
                Jinsi = jinsiList
            }  : null,
            })
        .FirstOrDefault();

            return victim; 
                }




    }


}
