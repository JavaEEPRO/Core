using arkAS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arkAS.Areas.Glushko.BLL
{
   public interface IRepository : IDisposable
    {
        #region System
        void Save();
        #endregion

        #region Documents
        IQueryable<gl_docs> GetDocuments();
        //gl_docs GetDocument();
        int SaveDocument(gl_docs element, bool withSave = true);
        bool DeleteDocument(int id);
        #endregion

        #region Doc Type
        IQueryable<gl_docTypes> GetDocumentTypes();
        bool DeleteDocumentType(int id);
        int SaveDocumentType(gl_docTypes element, bool withSave = true);
        #endregion

        #region Doc Status
        IQueryable<gl_docStatuses> GetDocumentStatuses();
        bool DeleteDocumentStatus(int id);
        int SaveDocumentStatus(gl_docStatuses element, bool withSave = true);
        #endregion

        #region
        IQueryable<fin_contragents> GetFinContragents();
        #endregion
    }
}
