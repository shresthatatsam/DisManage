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

        public string TotalDonationReceived()
        {
            var donationReceived = _context.donations.Where(x => x.Type == "Receive").Count().ToString();
            return donationReceived;
        }


        public string TotalDonationGiven()
        {
            var donationGiven = _context.donations.Where(x => x.Type == "Give").Count().ToString();
            return donationGiven;
        }


        public decimal TotalDonationReceivedAmount()
        {
            var donationReceived = _context.donations.Where(x => x.Type == "Receive").ToList();
            decimal amount = 0;
            foreach(var item in donationReceived)
            {
                amount += Convert.ToDecimal(item.amount);
            }
            return amount;
        }

        public decimal TotalDonationGivenAmount()
        {
            var donationReceived = _context.donations.Where(x => x.Type == "Give").ToList();
            decimal amount = 0;
            foreach (var item in donationReceived)
            {
                amount += Convert.ToDecimal(item.amount);
            }
            return amount;
        }
    }
}
