using arkAS.BLL;
using arkAS.Models;
using System;
using System.Collections.Generic;

namespace arkAS.Areas.Tsilurik.BLL
{
    public interface IDocumentManager : IDisposable
    {
        #region Documents
        List<tsilurik_documents> GetDocuments(ASCRUDGetItemsModel parameters, aspnet_Users user, out string msg, out int total);
        tsilurik_documents GetDocument(int id, aspnet_Users user, out string msg);
        tsilurik_documents CreateDocument(Dictionary<string, object> parameters, aspnet_Users user, out string msg);
        bool ChangeDocumentStatus(int id, string statusID, string name, aspnet_Users user, out string msg);
        List<tsilurik_types> GetDocumentTypes();
        List<tsilurik_status> GetDocumentStatuses();
        #endregion
    }
}