using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using arkAS.BLL;


namespace arkAS.Areas.Podvashetskyi.BLL
{
    public interface IManager : IDisposable
    {
        IContractorsManager Contractors { get; set; }
        IDocumentsManager Documents { get; set; }
        IInvoicesManager Invoices { get; set; }
        IMailManager Mails { get; set; }
        // get curent user
        aspnet_Users GetUser(); 
    }
}
