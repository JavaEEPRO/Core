using arkAS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arkAS.Areas.Vasilyev.BLL
{
    public interface IManager : IDisposable
    {
        #region System
        aspnet_Users GetUser(); 
        #endregion

        #region Managers Core
        IDocumentManager Document { get; set; }
        IInvoiceManager Invoice { get; set; }
        IMailManager Mail { get; set; }
        IContractorManager Contractor { get; set; }

        #endregion
    }
}
