using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Data;
using DataAccess.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess.Service
{
    public class VictimService:IVictimService
    {
        private readonly ApplicationDbContext _context;
        public VictimService(ApplicationDbContext context ) 
        {
            _context = context; 
        }

        public Victim getData(Guid id)
        {
           return _context.Victims.Where(x => x.Id == id).FirstOrDefault();
        }

        public Victim Create(Victim victim)
        {
            var existingRecord = _context.Victims.Where(x => x.ContactNumber == victim.ContactNumber).FirstOrDefault();
            victim.Secret_Number = GenerateSecretNumber();
            if (existingRecord != null)
            {
                existingRecord.Name = victim.Name ?? existingRecord.Name;
                existingRecord.Age = victim.Age ?? existingRecord.Age;
                existingRecord.Gender = victim.Gender ?? existingRecord.Gender;
                existingRecord.Email = victim.Email ?? existingRecord.Email;
                existingRecord.PPSizePhoto = victim.PPSizePhoto ?? existingRecord.PPSizePhoto;
                existingRecord.CitizenshipNumber = victim.CitizenshipNumber ?? existingRecord.CitizenshipNumber;
                existingRecord.NIDNumber = victim.NIDNumber ?? existingRecord.NIDNumber;
                existingRecord.FatherName = victim.FatherName ?? existingRecord.FatherName;
                existingRecord.MotherName = victim.MotherName ?? existingRecord.MotherName;
                existingRecord.GrandFatherName = victim.GrandFatherName ?? existingRecord.GrandFatherName;
                existingRecord.Secret_Number = victim.Secret_Number ?? existingRecord.Secret_Number;
                _context.SaveChanges();
                victim = existingRecord;
                return victim;
            }
            else
            {
               
                _context.Victims.Add(victim);
                _context.SaveChanges();
                return victim;
            }
       
        }

        private string GenerateSecretNumber()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public Victim Edit(Victim victim)
        {
            var existingRecord = _context.Victims.Where(x => x.Id == victim.Id).FirstOrDefault();

            if (existingRecord != null)
            {
                existingRecord.Name = victim.Name ?? existingRecord.Name;
                existingRecord.Age = victim.Age ?? existingRecord.Age;
                existingRecord.Gender = victim.Gender ?? existingRecord.Gender;
                existingRecord.Email = victim.Email ?? existingRecord.Email;
                existingRecord.PPSizePhoto = victim.PPSizePhoto ?? existingRecord.PPSizePhoto;
                existingRecord.CitizenshipNumber = victim.CitizenshipNumber ?? existingRecord.CitizenshipNumber;
                existingRecord.NIDNumber = victim.NIDNumber ?? existingRecord.NIDNumber;
                existingRecord.FatherName = victim.FatherName ?? existingRecord.FatherName;
                existingRecord.MotherName = victim.MotherName ?? existingRecord.MotherName;
                existingRecord.GrandFatherName = victim.GrandFatherName ?? existingRecord.GrandFatherName;

                _context.SaveChanges();
                victim = existingRecord;
                return victim;
            }
            else
            {
                _context.Victims.Add(victim);
                _context.SaveChanges();
                return victim;
            }

        }


        public Guid Delete(Guid id)
        {
            var VictimRecord = _context.Victims.Where(x => x.Id == id).FirstOrDefault();
            var LocationRecord = _context.Locations.Where(x => x.VictimId == id).FirstOrDefault();
            var DisasterImageRecord = _context.DisasterImages.Where(x => x.VictimId == id).ToList();
            var DisasterRecord = _context.Disasters.Where(x => x.VictimId == id).FirstOrDefault();

            VictimRecord.isActive =false;
            LocationRecord.isActive = false;
            foreach(var item in DisasterImageRecord)
            {
            item.isActive =false;    
            }
            DisasterRecord.isActive = false;
            _context.SaveChanges();
            return id;
        }

        public async Task<int> CountVictimsAsync()
        {
            // Count the number of victims asynchronously
            return await _context.Victims.CountAsync();
        }

    }
}
