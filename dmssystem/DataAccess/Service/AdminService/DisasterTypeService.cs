using DataAccess.Data;
using DataAccess.Service.AdminInterface;
using DataAccess.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service.AdminService
{
    public class DisasterTypeService : IDisasterType
    {
        public readonly ApplicationDbContext _context;

        public DisasterTypeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public DisasterType create(DisasterType disasterTypeService)
        {
            var existingRecord = _context.disasterTypes.Where(x => x.Id == disasterTypeService.Id).FirstOrDefault();

            if (existingRecord != null)
            {
                existingRecord.DisasterName = disasterTypeService.DisasterName ?? existingRecord.DisasterName;
                existingRecord.Severity = disasterTypeService.Severity ?? existingRecord.Severity;
                existingRecord.Isactive = true;

                _context.SaveChanges();
                return existingRecord;
            }
            else
            {
                disasterTypeService.Isactive = true;
                _context.disasterTypes.Add(disasterTypeService);
                _context.SaveChanges();
                return disasterTypeService;
            }

        }

        public List<DisasterType> GetAll()
        {
            return _context.disasterTypes.Where(x=>x.Isactive == true).ToList();
        }

        public DisasterType getData(Guid id)
        {
            return _context.disasterTypes.Where(x => x.Id == id).FirstOrDefault();
        }

        public Guid Delete(Guid id)
        {
           
            var DisasterType = _context.disasterTypes.Where(x => x.Id == id).FirstOrDefault();

            DisasterType.Isactive = false;
       
            _context.SaveChanges();
            return id;
        }
    }
}
