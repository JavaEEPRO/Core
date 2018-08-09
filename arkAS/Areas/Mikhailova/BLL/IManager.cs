using arkAS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arkAS.Areas.Mikhailova.BLL
{
    public interface IManager : IDisposable
    {
        IContractsManager Contracts { get; set; }
        IInvoicesManager Invoices { get; set; }
        IMailsManager Mails { get; set; }
        aspnet_Users GetUser();     // get curent user
    }
}
