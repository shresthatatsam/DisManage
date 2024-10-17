using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DataAccess.Service.Interface
{
    public interface IImage
    {
        Task<List<Image>> Create(List<Image> images);
        Image getData(Guid id);
    }
}
