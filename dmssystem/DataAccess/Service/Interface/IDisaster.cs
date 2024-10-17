using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DataAccess.Service.Interface
{
    public interface IDisaster
    {
        Disaster Create(Disaster disaster);
        Disaster getData(Guid id);
    }
}
