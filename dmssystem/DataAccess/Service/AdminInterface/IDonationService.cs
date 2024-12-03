using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DataAccess.Service.AdminInterface
{
    public interface IDonationService
    {
        Donation Create(Donation donation, List<JinsiDonation> jinsiDonation);
        string TotalDonationReceived();
        string TotalDonationGiven();
        decimal TotalDonationReceivedAmount();
        decimal TotalDonationGivenAmount();
    }
}
