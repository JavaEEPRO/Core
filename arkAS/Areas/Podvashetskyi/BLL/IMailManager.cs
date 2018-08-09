using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using arkAS.BLL;

namespace arkAS.Areas.Podvashetskyi.BLL
{
    public interface IMailManager : IDisposable
    {
        #region Mails
        List<pdv_mails> GetMails();
        pdv_mails GetMail(int id);
        void SaveMail(pdv_mails item);
        bool CreateMail(int contractorFromID, int contractorToID);
        bool DeleteMail(int id, out string msg, pdv_contractors user);
        #endregion
        #region Delivery Mail Statuses
        List<pdv_deliveryMailStatuses> GetDeliveryMailStatuses();
        pdv_deliveryMailStatuses GetDeliveryMailStatus(int id);
        int SaveDeliveryMailStatus(pdv_deliveryMailStatuses item, bool withSave = true);
        bool DeleteDeliveryMailStatus(int id);
        bool EditDeliveryMailStatusField(int id, string code, string value, out string msg);
        #endregion
        #region Delivery Service
        List<pdv_deliveryService> GetDeliveryServices();
        pdv_deliveryService GetDeliveryService(int id);
        int SaveDeliveryService(pdv_deliveryService item, bool withSave = true);
        bool DeleteDeliveryService(int id);
        bool EditDeliveryServiceField(int id, string code, string value, out string msg);
        #endregion
    }
}
