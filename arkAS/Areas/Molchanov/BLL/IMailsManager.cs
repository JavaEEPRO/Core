using arkAS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arkAS.Areas.Molchanov.BLL
{
    public interface IMailsManager : IDisposable
    {
        #region Mails
        List<molchanov_mails> GetMails(out string msg, aspnet_Users user);
        molchanov_mails GetMail(int id, out string msg, aspnet_Users user);
        molchanov_mails CreateMail(Dictionary<string, string> parameters, out string msg, aspnet_Users user);
        molchanov_mails EditMail(Dictionary<string, string> parameters, int id, out string msg, aspnet_Users user);
        bool ChangeMailsStatus(int id, string name, string value, out string msg, aspnet_Users user);
        bool RemoveMail(int id, out string msg, aspnet_Users user);
        List<molchanov_logMails> GetMailLogsByID(int id);
        #endregion
        #region MailsStauses
        List<molchanov_mailStatuses> GetMailStatuses(out string msg, aspnet_Users user);
        molchanov_mailStatuses GetMailStatus(int id, out string msg, aspnet_Users user);
        molchanov_mailStatuses CreateMailStatus(Dictionary<string, string> parameters, out string msg, aspnet_Users user);
        molchanov_mailStatuses EditMailStatus(Dictionary<string, string> parameters, int id, out string msg, aspnet_Users user);
        bool RemoveMailStatus(int id, out string msg, aspnet_Users user);
        #endregion
        #region DeliverySystems
        List<molchanov_deliverySystems> GetDeliverySystems(out string msg, aspnet_Users user);
        molchanov_deliverySystems GetDeliverySystem(int id, out string msg, aspnet_Users user);
        molchanov_deliverySystems CreateDeliverySystem(Dictionary<string, string> parameters, out string msg, aspnet_Users user);
        molchanov_deliverySystems EditDeliverySystem(Dictionary<string, string> parameters, int id, out string msg, aspnet_Users user);
        bool RemoveDeliverySystem(int id, out string msg, aspnet_Users user);
        #endregion

    }
}
