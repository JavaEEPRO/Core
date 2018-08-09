using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using arkAS.BLL;

namespace arkAS.Areas.Motskin.BLL
{
    public interface IManager:IDisposable
    {
        IDocumentsManager Documents { get; set; }
        IInvoicesManager Invoices { get; set; }
        IMailManager Mail { get; set; }
        IContractorsManager Contractors { get; set; }
        aspnet_Users GetUser();
    }
}
