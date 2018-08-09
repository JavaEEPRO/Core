using arkAS.Areas.Vasilyev.Models;
using arkAS.BLL;
using arkAS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arkAS.Areas.Vasilyev.BLL
{
    public interface IDocumentManager : IDisposable
    {
        #region Documents
        vas_documents GetDocument(int id, aspnet_Users user, out string msg);
        List<vas_documents> GetDocuments(ASCRUDGetItemsModel parameters, aspnet_Users user, out string msg);
        vas_documents CreateDocument(string name, int typeID, int contractorID, aspnet_Users user, out string msg);
        bool EditDocumentField(int pk, string name, string value, aspnet_Users user, out string msg);
        bool DeleteDocument(int id, aspnet_Users user, out string msg);
        #endregion

        #region Document Statuses
        vas_docStatuses GetDocStatus(int id);
        List<vas_docStatuses> GetDocStatuses();
        vas_docStatuses CreateDocStatus(string name, string code);
        void DeleteDocStatus(int id);
        #endregion

        #region Document Types
        vas_docTypes GetDocType(int id);
        List<vas_docTypes> GetDocTypes();
        vas_docTypes CreateDocType(string name, string code);
        void DeleteDocType(int id);
        #endregion
    }
}