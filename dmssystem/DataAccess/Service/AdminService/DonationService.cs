using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Data;
using DataAccess.Service.AdminInterface;
using DataAccess.Service.Interface;
using Models;

namespace DataAccess.Service.AdminService
{
    public class DonationService :IDonationService
    {
        public readonly ApplicationDbContext _context;
        private readonly IJinsiService _jinsiService;
      
        public DonationService(ApplicationDbContext context, IJinsiService jinsiService)
        {
            _context = context;
            _jinsiService = jinsiService;
        }
        public Donation Create(Donation donation)
        {
            if(donation.Type == "Receive")
            {
                var existingDonation = _context.donations
            .FirstOrDefault(d => d.SecretNumber == donation.SecretNumber);

                if (existingDonation != null)
                {
                    return existingDonation;
                }

                _context.donations.Add(donation);
                _context.SaveChanges();
                return donation;

            }
            else
            {

           Victim victims = _context.Victims.Where(x => x.Secret_Number == donation.SecretNumber).FirstOrDefault();

            donation.VictimId = victims.Id;

            for(int i=0; i< donation.Jinsi.Count; i++)
            {
                if (!HasSufficientQuantity(Guid.Parse(donation.Jinsi[i]) , (float)donation.Quantity[i]))
                {
                    throw new InvalidOperationException("Not enough stock available for the donation.");
                }
            }
           

            var existingDonation = _context.donations
        .FirstOrDefault(d => d.SecretNumber == donation.SecretNumber);

            if (existingDonation != null)
            {
                // Optionally return the existing donation or handle it as needed
                return existingDonation; // or throw an exception, or return null
            }

            // If no existing donation, add the new donation
            _context.donations.Add(donation);

            if (donation.VictimId != Guid.Empty)
            {
                var victim = _context.Victims.FirstOrDefault(v => v.Id == donation.VictimId);
                if (victim != null)
                {
                    victim.DonationId = donation.id;
                    _context.SaveChanges();
                }
            }
            _context.SaveChanges();
            return donation;

            }
        }
        public bool HasSufficientQuantity(Guid jinsiId, float requestedQuantity)
        {
            // Get the product by its ID
            var product = _context.jinsiDonations.FirstOrDefault(p => p.id == jinsiId);

            if (product == null)
            {
                // Product doesn't exist, return false
                return false;
            }
            if (product.Quantity >= requestedQuantity)
            {
                // Decrease the product quantity
                product.Quantity -= requestedQuantity;

                // Save changes to the database
                _context.SaveChanges();

                return true; // Return true after successfully updating the quantity
            }
            else
            {
                // Throw an exception if the quantity is insufficient
                throw new InvalidOperationException("Insufficient quantity available.");
            }
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
