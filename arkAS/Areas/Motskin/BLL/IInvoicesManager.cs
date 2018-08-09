using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using arkAS.BLL;

namespace arkAS.Areas.Motskin.BLL
{
    public interface IInvoicesManager :
        IBaseCRUDManager<motskin_invoices>,
        IBaseStatusUseManager<motskin_invoiceStatuses, motskin_invoiceStatusLog>
    {

    }
}
