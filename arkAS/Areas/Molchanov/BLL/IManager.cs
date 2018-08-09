using arkAS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arkAS.Areas.Molchanov.BLL
{
    public interface IManager : IDisposable
    {
        IContragentsManager Contragents { get; set; }
        IDocumentsManager Documents { get; set; }
        IInvoicesManager Invoices { get; set; }
        IMailsManager Mails {get; set;}
        aspnet_Users GetUser();

    }
}
