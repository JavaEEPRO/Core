using arkAS.BLL;
using arkAS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arkAS.Areas.Vasilyev.BLL
{
    public interface IMailManager : IDisposable
    {
        #region Mails
        vas_mails GetMail(int id, aspnet_Users user, out string msg);
        List<vas_mails> GetMails(ASCRUDGetItemsModel parameters, aspnet_Users user, out string msg);
        vas_mails CreateMail(string from, string to, aspnet_Users user, out string msg);
        bool EditMailField(int pk, string name, string value, aspnet_Users user, out string msg);
        bool DeleteMail(int id, aspnet_Users user, out string msg);
        #endregion

        #region Mail Statuses
        vas_mailStatuses GetMailStatus(int id);
        List<vas_mailStatuses> GetMailStatuses();
        vas_mailStatuses CreateMailStatus(string name, string code);
        void DeleteMailStatus(int id);
        #endregion
    }
}