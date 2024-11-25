using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DataAccess.Service.AdminInterface
{
    public interface IJinsiService
    {
        JinsiDonation Create(JinsiDonation Jinsidonation);
        List<JinsiDonation> getAllJinsi(JinsiDonation Jinsidonation);
    }
}
