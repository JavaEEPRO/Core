using arkAS.BLL;
using System;


namespace arkAS.Areas.Tsilurik.BLL
{
    public interface IManager : IDisposable
    {
        #region Managers
        IDocumentManager Document { get; set; }
        IInvoiceManager Invoice { get; set; }
        IMailManager Mail { get; set; }
        IContractorManager Contractor { get; set; }
        aspnet_Users GetUser();
        #endregion
    }
}
