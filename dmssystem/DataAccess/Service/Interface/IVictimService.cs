using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DataAccess.Service.Interface
{
    public interface IVictimService
    {
        Victim Create(Victim victim);
        Victim Edit(Victim victim);
        Guid Delete(Guid id);
        Victim getData(Guid id);
        Task<int> CountVictimsAsync();
    }
}
