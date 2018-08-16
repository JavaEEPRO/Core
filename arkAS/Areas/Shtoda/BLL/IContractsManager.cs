using arkAS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arkAS.Areas.Shtoda.BLL
{
    public interface IContractsManager : IDisposable
    {
        #region Contracts
        List<Shtoda_contracts> GetContracts();
        Shtoda_contracts GetContract(int id);
        void SaveContract(Shtoda_contracts item);
        bool CreateContract(string name);
        bool EditContract(aspnet_Users user, int id, string value, string name, out string msg);

        bool DeleteContract(aspnet_Users user, int id, out string msg);
        bool EditContractField(aspnet_Users userRol, int id, string code, string value, out string msg, Shtoda_contracts user);
        bool IsCanCurrentUserChange();

        #endregion

        #region ContractStatus
        bool EditContractStatusField(aspnet_Users user,int pk, string name, string value, out string msg);
        List<Shtoda_statuses_contracts> GetStatusesContract();
        bool DeleteStatusContract(aspnet_Users user, int id, out string msg);
        int SaveStatusContract(Shtoda_statuses_contracts item, bool withSave = true);
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
