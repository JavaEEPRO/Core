using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using arkAS.BLL;

namespace arkAS.Areas.Udovika.BLL
{
    public interface IDocumentsManager : IDisposable
    {
        #region Documents
        List<udovika_contract> GetDocuments();
        udovika_contract GetDocument(int id, aspnet_Users user, out string msg);
        bool CreateDocument(string name, aspnet_Users user, out string msg);
        int SaveDocument(udovika_contract item, aspnet_Users user, out string msg);
        bool EditDocument(int id, string value, aspnet_Users user, out string msg);
        bool DeleteDocument(int id, aspnet_Users user, out string msg);
        bool EditDocumentField(int id, string code, string value,
            out string msg, aspnet_Users user);
        #endregion
        #region Status
        udovika_statusContract GetStatusDocument(int id);
        int SaveSatusDocument(udovika_statusContract status);
        bool EditDocumentStatusField(int id, string name, 
            string value, out string msg);
        List<udovika_statusContract> GetDocumentStatuses();
        #endregion
        #region Type
        List<udovika_contractType> GetDocumentTypes();
        udovika_contractType GetDocumentType(int id);
        int SaveDocumentType(udovika_contractType item);
        bool EditDocumentTypeField(int id, string code, 
            string value, out string msg);
        #endregion
    }
}
