using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using arkAS.BLL;

namespace arkAS.Areas.Motskin.BLL
{
    public interface IContractorsManager: 
        IBaseCRUDManager<motskin_contractors>
    {
        List<motskin_contractors> GetAll();
    }
}
