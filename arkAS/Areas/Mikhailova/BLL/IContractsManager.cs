using arkAS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arkAS.Areas.Mikhailova.BLL
{
    public interface IContractsManager : IDisposable
    {
        #region Contracts
        List<Mikhailova_contracts> GetContracts();
        Mikhailova_contracts GetContract(int id);
        void SaveContract(Mikhailova_contracts item);
        bool CreateContract(string name);
        bool EditContract(aspnet_Users user, int id, string value, string name, out string msg);

        bool DeleteContract(aspnet_Users user, int id, out string msg);
        bool EditContractField(aspnet_Users userRol, int id, string code, string value, out string msg, Mikhailova_contracts user);
        bool IsCanCurrentUserChange();

        #endregion

        #region ContractStatus
        bool EditContractStatusField(aspnet_Users user,int pk, string name, string value, out string msg);
        List<Mikhailova_statuses_contracts> GetStatusesContract();
        bool DeleteStatusContract(aspnet_Users user, int id, out string msg);
        int SaveStatusContract(Mikhailova_statuses_contracts item, bool withSave = true);
        #endregion

        #region DocType
        Mikhailova_docTypes GetDocType(int id);
        List<Mikhailova_docTypes> GetDocTypes();
        void SaveDocType(Mikhailova_docTypes item);
        #endregion

        #region DocTypeTemplates
        Mikhailova_docTypeTemplates GetDocTypeTemplate(int id);
        List<Mikhailova_docTypeTemplates> GetDocTypeTemplates();
        List<Mikhailova_docTypeTemplates> GetListTemplatesByType(int typeId);
        void SaveDocTypeTemplate(Mikhailova_docTypeTemplates item);
        void DeleteDocTypeTemplate(int id);
        void EditTemplateField(int pk, string name, string value);
        #endregion

        #region Contagents
        List<Mikhailova_contagents> GetContagents();
        Mikhailova_contagents GetContagent(int id);
        void SaveContagent(Mikhailova_contagents item);
        void DeleteContagent(int id);
        #endregion
    }
}
