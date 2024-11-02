using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Data;
using DataAccess.Service.AdminInterface;
using Models;

namespace DataAccess.Service.AdminService
{
    public class DonationService :IDonationService
    {
        public readonly ApplicationDbContext _context;

        public DonationService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Donation Create(Donation donation)
        {
            _context.donations.Add(donation);
            _context.SaveChanges();
            return donation;

        }

    }
}
