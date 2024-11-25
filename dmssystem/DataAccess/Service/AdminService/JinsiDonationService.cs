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
    public class JinsiDonationService : IJinsiService
    {
        public readonly ApplicationDbContext _context;

        public JinsiDonationService(ApplicationDbContext context)
        {
            _context = context;
        }
        public JinsiDonation Create(JinsiDonation Jinsidonation)
        {
           
            _context.jinsiDonations.Add(Jinsidonation);
            _context.SaveChanges();
            return Jinsidonation;

        }
    }
}
