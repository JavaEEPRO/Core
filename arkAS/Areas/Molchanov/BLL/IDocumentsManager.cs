using arkAS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arkAS.Areas.Molchanov.BLL
{
    public interface IDocumentsManager: IDisposable
    {
        #region Documents
        List<molchanov_documents> GetDocuments(out string msg, aspnet_Users user);
        molchanov_documents GetDocument(int id, out string msg, aspnet_Users user);
        molchanov_documents CreateDocument(Dictionary<string, string> parameters, out string msg, aspnet_Users user);
        molchanov_documents EditDocument(Dictionary<string, string> parameters, int id, out string msg, aspnet_Users user);
        bool ChangeDocumentInline(int id, string name, string value, out string msg, aspnet_Users user);
        bool RemoveDocument(int id, out string msg, aspnet_Users user);
        List<molchanov_logDocuments> GetDocLogsByID(int id);
        #endregion
        #region DocTypes
        List<molchanov_docTypes> GetDocTypes(out string msg, aspnet_Users user);
        molchanov_docTypes GetDocType(int id, out string msg, aspnet_Users user);
        molchanov_docTypes CreateDocType(Dictionary<string, string> parameters, out string msg, aspnet_Users user);
        molchanov_docTypes EditDocType(Dictionary<string, string> parameters, int id, out string msg, aspnet_Users user);
        bool RemoveDocType(int id, out string msg, aspnet_Users user);
        #endregion
        #region DocStatuses
        List<molchanov_docStatuses> GetDocStatuses(out string msg, aspnet_Users user);
        molchanov_docStatuses GetDocStatus(int id, out string msg, aspnet_Users user);
        molchanov_docStatuses CreateDocStatus(Dictionary<string, string> parameters, out string msg, aspnet_Users user);
        molchanov_docStatuses EditDocStatus(Dictionary<string, string> parameters, int id, out string msg, aspnet_Users user);
        bool RemoveDocStatus(int id, out string msg, aspnet_Users user);
        #endregion

    }
}
