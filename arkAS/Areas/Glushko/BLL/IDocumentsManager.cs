using arkAS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arkAS.Areas.Glushko.BLL
{
    public interface IDocumentsManager : IDisposable
    {
        #region Doc
        List<gl_docs> GetDocuments();
        gl_docs GetDocument(int id);

        void SaveDocument(gl_docs item);
        bool DeleteDocument(int id, out string msg);
        //bool EditDocumentField(int pk, string name, string value);
        bool EditDocumentField(int id, string code, string value, out string msg);
        #endregion

        #region Doc Statuses
        List<gl_docStatuses> GetDocumentStatuses();
        gl_docStatuses GetDocumentStatus(int id);
        int SaveDocumentStatus(gl_docStatuses item, bool withSave = true);
        bool DeleteDocumentStatus(int id,out string msg);
        bool EditDocumentStatusField(int pk, string name, string value, out string msg);
        #endregion

        #region Doc Type
        List<gl_docTypes> GetDocumentTypes();
        gl_docTypes GetDocumentType(int id);
        int SaveDocumentType(gl_docTypes item, bool withSave = true);
        bool DeleteDocumentType(int id);
        // bool EditDocumentTypeField(int pk, string name, string value, out string msg);
        #endregion

        #region
        List<fin_contragents> GetFinContragents();
        #endregion
    }
}

