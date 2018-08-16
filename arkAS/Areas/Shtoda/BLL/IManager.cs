using arkAS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arkAS.Areas.Shtoda.BLL
{
    public interface IManager : IDisposable
    {
        IActsManager Acts { get; set; }
        IContractsManager Contracts { get; set; }
        IInvoicesManager Invoices { get; set; }
        IAddonsManager Addons { get; set; }
        IMailsManager Mails { get; set; }
        aspnet_Users GetUser();     
    }
}
