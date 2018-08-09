using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using arkAS.BLL;

namespace arkAS.Areas.Udovika.BLL
{
    public interface IManager : IDisposable
    {
        aspnet_Users GetUser();
        IDocumentsManager Document { get; set; }
        IContractorsManager Contractor { get; set; }
        IInvoicesManager Invoice { get; set; }
        IMailsManager Mail { get; set; }
    }
}
