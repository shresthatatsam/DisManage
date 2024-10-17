﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Data;
using DataAccess.Service.Interface;
using Models;

namespace DataAccess.Service
{
    public class DashboardService :IDashboard
    {
        public readonly ApplicationDbContext _context;

               public DashboardService(ApplicationDbContext context)
               {
                   _context = context;
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
            var victim = _context.Victims
        .Where(x => x.Id == id)
        .Select(v => new Victim
        {
            Id = v.Id,
            Name = v.Name,
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
                PermanentProvince = v.Location.PermanentProvince,
                PermanentDistrict = v.Location.PermanentDistrict,
                PermanentMunicipality = v.Location.PermanentMunicipality,
                PermanentTole = v.Location.PermanentTole,
                TemporaryProvince = v.Location.TemporaryProvince,
                TemporaryDistrict = v.Location.TemporaryDistrict,
                TemporaryMunicipality = v.Location.TemporaryMunicipality,
                TemporaryTole = v.Location.TemporaryTole,
                isActive = v.Location.isActive,
                created_at = v.Location.created_at
            } : null, // Null check in case Location is not present

            Disaster = v.Disaster != null ? new Disaster
            {
                Id = v.Disaster.Id,
                DisasterType = v.Disaster.DisasterType,
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
            }).ToList() : new List<Image>() // Return images related to the victim
        })
        .FirstOrDefault();

            return victim; 
                }

    }
}
