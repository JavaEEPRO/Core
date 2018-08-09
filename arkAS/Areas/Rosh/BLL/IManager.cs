using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using arkAS.BLL;

namespace arkAS.Areas.Rosh.BLL
{
    public interface IManager: IDisposable
    {
        IContragentsManager Contragents { get; set; }
        IOrderManager Orders { get; set; }
        IDocumentsManager Documents { get; set; }
        IMailsManager Mails { get; set; }
        aspnet_Users GetUser();
    }
}
