using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using arkAS.BLL;

namespace arkAS.Areas.Podvashetskyi.BLL
{
    public interface IContractorsManager : IDisposable
    {
        List<pdv_contractors> GetContractors(aspnet_Users user, out string msg);
        List<pdv_contractors> GetContractors();
        pdv_contractors GetContractor(int id);
        void SaveContractor(pdv_contractors item);
        bool CreateContractor(string name);
        bool EditContractorsField(int id, string code, string value, out string msg);
        bool DeleteContractor(int id, out string msg);
    }
}
