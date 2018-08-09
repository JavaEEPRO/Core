using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using arkAS.BLL;

namespace arkAS.Areas.Podvashetskyi.BLL
{
    public interface IDocumentsManager : IDisposable
    {
        #region Document
        List<pdv_documents> GetDocuments();
        pdv_documents GetDocument(int id);
        void SaveDocument(pdv_documents item);
        bool AddDocument(string number);
        bool EditDocumentField(int id, string code, string value, aspnet_Users user, out string msg);
        bool DeleteDocument(int id, out string msg);
        #endregion
        #region Document Statuses
        List<pdv_documentStatuses> GetDocumentStatuses();
        pdv_documentStatuses GetDocumentStatus(int id);
        int GetDocumentStatusCodeId(string code);
        int SaveDocumentStatus(pdv_documentStatuses item, bool withSave = true);
        bool DeleteDocumentStatus(int id);
        bool EditDocumentStatusField(int pk, string name, string value, out string msg);
        #endregion
        #region Document Type
        List<pdv_documentType> GetDocumentTypes();
        pdv_documentType GetDocumentType(int id);
        int SaveDocumentTypes(pdv_documentType item, bool withSave = true);
        bool DeleteDocumentTypes(int id);
        bool EditDocumentTypesField(int pk, string name, string value, out string msg);
        #endregion 
    }
}
