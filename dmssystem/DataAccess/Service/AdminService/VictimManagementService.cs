using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Data;
using DataAccess.Service.AdminInterface;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess.Service.AdminService
{
	public class VictimManagementService : IVictimManagement
	{
		public readonly ApplicationDbContext _context;

		public VictimManagementService(ApplicationDbContext context)
		{
			_context = context;
		}

		public List<Victim> recentDisaster()
		{
            var victim = _context.Victims
    .AsNoTracking().Where(x=>x.isActive == true)
    .OrderByDescending(x => x.created_at)
    .Select(v => new Victim
    {
        Id = v.Id,
        Name = v.Name,
        //Age = v.Age,
        //Gender = v.Gender,
        ContactNumber = v.ContactNumber,
        Email = v.Email,
        //PPSizePhoto = v.PPSizePhoto,
        //CitizenshipNumber = v.CitizenshipNumber,
        //NIDNumber = v.NIDNumber,
        //FatherName = v.FatherName,
        //MotherName = v.MotherName,
        //GrandFatherName = v.GrandFatherName,
        isActive = v.isActive,
        created_at = v.created_at,
    }).ToList();


            return victim; // Return the data instead of null
		}
	}
}
