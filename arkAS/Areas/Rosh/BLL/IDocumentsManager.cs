using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using arkAS.BLL;

namespace arkAS.Areas.Rosh.BLL
{
    public interface IDocumentsManager: IDisposable
    {
        #region System
        // void Save();
        #endregion

        #region documents
        List<rosh_documents> GetDocuments();
        rosh_documents GetDocument(int id);
        void SaveDocument(rosh_documents document, aspnet_Users user, out string msg);
        void DeleteDocument(int id, aspnet_Users user, out string msg);
        void EditDocField(int pk, string name, string value);
        #endregion

        #region docTypes
        List<rosh_docTypes> GetDocTypes();
        rosh_docTypes GetDocType(int id);
        void SaveDocType(rosh_docTypes docType);
        void DeleteDocType(int id);
        #endregion

        #region docStatuses
        List<rosh_docStatuses> GetDocStatuses();
        rosh_docStatuses GetDocStatus(int id);
        void SaveDocStatus(rosh_docStatuses docStatus);
        void DeleteDocStatus(int id);
        #endregion

        #region docLogs
        List<rosh_docLogs> GetDocLogs();
        rosh_docLogs GetDocLog(int id);
        void SaveDocLog(rosh_docLogs docLog);
        void DeleteDocLog(int id);
        #endregion

    }
}
