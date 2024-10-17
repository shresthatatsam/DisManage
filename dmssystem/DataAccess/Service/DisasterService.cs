using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Data;
using DataAccess.Service.Interface;
using Models;

namespace DataAccess.Service
{
    public class DisasterService:IDisaster
    {
        private readonly ApplicationDbContext _context;
        public DisasterService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Disaster getData(Guid id)
        {
            return _context.Disasters.Where(x => x.VictimId == id).FirstOrDefault();
        }

        public Disaster Create(Disaster disaster)
        {

            var existingDisaster = _context.Disasters
       .FirstOrDefault(d => d.VictimId == disaster.VictimId);

            if (existingDisaster != null)
            {
                existingDisaster.DisasterType = disaster.DisasterType ?? existingDisaster.DisasterType;
                existingDisaster.Description = disaster.Description ?? existingDisaster.Description;
                existingDisaster.DateReported = disaster.DateReported != default ? disaster.DateReported : existingDisaster.DateReported;
               
                _context.SaveChanges();
                if (disaster.VictimId != Guid.Empty)
                {
                    var victim = _context.Victims.FirstOrDefault(v => v.Id == disaster.VictimId);
                    if (victim != null)
                    {
                        victim.DisasterId = existingDisaster.Id;
                        _context.SaveChanges();
                    }
                }
                return existingDisaster;
            }
            else
            {

                _context.Disasters.Add(disaster);
                _context.SaveChanges();
                if (disaster.VictimId != Guid.Empty)
                {
                    var victim = _context.Victims.FirstOrDefault(v => v.Id == disaster.VictimId);
                    if (victim != null)
                    {
                        victim.DisasterId = disaster.Id;
                        _context.SaveChanges();
                    }
                }
                return disaster;
            }
        }

    }
}
