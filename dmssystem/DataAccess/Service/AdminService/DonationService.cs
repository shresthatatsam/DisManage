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
    public class DonationService : IDonationService
    {
        public readonly ApplicationDbContext _context;
        private readonly IJinsiService _jinsiService;

        public DonationService(ApplicationDbContext context, IJinsiService jinsiService)
        {
            _context = context;
            _jinsiService = jinsiService;
        }
        public Donation Create(Donation donation, List<JinsiDonation> jinsiDonation)
        {
            if (donation.Type == "Receive")
            {
               

                _context.donations.Add(donation);

                _context.SaveChanges();
             
                if (jinsiDonation != null && jinsiDonation.Any())
                {
                    // Now, add the corresponding JinsiDonation records if the list is not empty
                    foreach (var j in jinsiDonation)
                    {
                        if (string.IsNullOrEmpty(j.jBrand) || string.IsNullOrEmpty(j.jCost) || string.IsNullOrEmpty(j.jname))
                        {
                            // Skip this entry and continue with the next iteration
                            continue;
                        }
                        // Set the DonationId for each JinsiDonation (assuming the foreign key is called 'DonationId')
                        j.DonationId = donation.id;  // Assuming Donation has an Id field

                        // Add the fields for JinsiDonation
                        _context.jinsiDonations.Add(new JinsiDonation
                        {
                            jname = j.jname,  // Copying the value of jname
                            jCost = j.jCost,  // Nullable field, it can be null
                            jBrand = j.jBrand,  // Copying the value of jBrand
                            jQuantity = j.jQuantity,  // Copying the value of jQuantity
                            jKaifayat = j.jKaifayat,  // Copying the value of jKaifayat
                            jSource = j.jSource,  // Copying the value of jSource
                            DonationId = donation.id // Set the foreign key relationship
                        });
                    }

                    // Save changes to save JinsiDonations
                    _context.SaveChanges();
                }
                else
                {
                    // Log or handle the case where the list is empty (if needed)
                    Console.WriteLine("No JinsiDonation data to save.");
                }


                // Save changes to save JinsiDonations
                _context.SaveChanges();
                return donation;

            }
            else
            {

                Victim victims = _context.Victims.Where(x => x.Secret_Number == donation.SecretNumber).FirstOrDefault();

                donation.VictimId = victims.Id;

                for (int i = 0; i < donation.Jinsi.Count; i++)
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
            if (product.jQuantity >= requestedQuantity)
            {
                // Decrease the product quantity
                product.jQuantity -= requestedQuantity;

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
            foreach (var item in donationReceived)
            {
                foreach (var amo in item.amount)
                {
                    amount += Convert.ToDecimal(amo);
                }
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
