using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using arkAS.BLL;

namespace arkAS.Areas.Udovika.BLL
{
    public interface IContractorsManager : IDisposable
    {
        List<udovika_contractor> GetContractors();
        List<udovika_contractor> GetContractors2();
        udovika_contractor GetContractor(int id);
    }
}
