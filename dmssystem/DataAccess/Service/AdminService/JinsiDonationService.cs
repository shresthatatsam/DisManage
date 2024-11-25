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

        public List<JinsiDonation> getAllJinsi(JinsiDonation Jinsidonation)
        {
           return _context.jinsiDonations.Select(x => new JinsiDonation
            {
               Brand = x.Brand,
               Cost = x.Cost,
               id = x.id,
               Kaifayat = x.Kaifayat,
               name = x.name,
               Quantity = x.Quantity,
               Source = x.Source,

            }).ToList();

        }
    }
}
