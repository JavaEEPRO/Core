using arkAS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arkAS.Areas.Fedchenko.BLL
{
    interface IDocumentsManager:IDisposable
    {
        #region Documents
        List<fedchenko_documents> GetDocuments();
        fedchenko_documents GetDocument(int id);
        void SaveDocument(fedchenko_documents doc);
        bool CreateDocument(string name);
        bool EditDocument(int id, string value);
        bool DeleteContract(int id, out string message);
        bool EditDocumentField(int id, string code, string value, out string message, fedchenko_documents user);
        #endregion

        #region DocumentsStatus
        bool EditDocumentStatusField(int pk, string name, string value, out string msg);
        List<fedchenko_docStatus> GetDocumentStatuses();
        bool DeleteStatusContract(int id);
        int SaveDocumentStatus(fedchenko_docStatus item, bool withSave = true); 
        #endregion

    }
}
