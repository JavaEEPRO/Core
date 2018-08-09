using arkAS.BLL;
using arkAS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arkAS.Areas.Vasilyev.BLL
{
    public interface IContractorManager : IDisposable
    {
        vas_contractors GetContractor(int id, aspnet_Users user, out string msg);
        List<vas_contractors> GetContractors(ASCRUDGetItemsModel parameters, aspnet_Users user, out string msg);
        List<vas_contractors> GetContractors();
        vas_contractors CreateContractor(string name, aspnet_Users user, out string msg);
        bool EditContractorField(int pk, string name, string value, aspnet_Users user, out string msg);
        bool DeleteContractor(int id, aspnet_Users user, out string msg);
    }
}