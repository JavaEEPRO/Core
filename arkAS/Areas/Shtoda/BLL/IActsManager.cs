using arkAS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arkAS.Areas.Shtoda.BLL
{  
    public interface IActsManager : IDisposable
    {
        #region Acts
        List<Shtoda_acts> GetActs();
        Shtoda_acts GetAct(int id);
        void SaveAct(Shtoda_acts item);
        bool CreateAct(string name);
        bool EditAct(aspnet_Users user, int id, string value, string name, out string msg);

        bool DeleteAct(aspnet_Users user, int id, out string msg);
        bool EditActField(aspnet_Users userRol, int id, string code, string value, out string msg, Shtoda_acts user);
        bool IsCanCurrentUserChange();

        #endregion

        #region ActStatus
        bool EditActStatusField(aspnet_Users user,int pk, string name, string value, out string msg);
        List<Shtoda_statuses_acts> GetStatusesAct();
        bool DeleteStatusAct(aspnet_Users user, int id, out string msg);
        int SaveStatusAct(Shtoda_statuses_acts item, bool withSave = true);
        #endregion

        #region DocType
        Shtoda_docTypes GetDocType(int id);
        List<Shtoda_docTypes> GetDocTypes();
        void SaveDocType(Shtoda_docTypes item);
        #endregion

        #region DocTypeTemplates
        Shtoda_docTypeTemplates GetDocTypeTemplate(int id);
        List<Shtoda_docTypeTemplates> GetDocTypeTemplates();
        List<Shtoda_docTypeTemplates> GetListTemplatesByType(int typeId);
        void SaveDocTypeTemplate(Shtoda_docTypeTemplates item);
        void DeleteDocTypeTemplate(int id);
        void EditTemplateField(int pk, string name, string value);
        #endregion

        #region Contragents
        List<Shtoda_contragents> GetContragents();
        Shtoda_contragents GetContragent(int id);
        void SaveContragent(Shtoda_contragents item);
        void DeleteContragent(int id);
        #endregion
    }
}
