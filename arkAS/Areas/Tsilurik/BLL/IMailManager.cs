using arkAS.BLL;
using arkAS.Models;
using System;
using System.Collections.Generic;

namespace arkAS.Areas.Tsilurik.BLL
{
    public interface IMailManager : IDisposable
    {
        #region Mail
        List<tsilurik_mails> GetMails(ASCRUDGetItemsModel parameters, aspnet_Users user, out string msg,out int total);
        tsilurik_mails GetMail(int id, aspnet_Users user, out string msg);
        tsilurik_mails CreateMail(Dictionary<string,object> parameters, aspnet_Users user, out string msg);
        bool ChangeMailStatus(int id, string statusID, string name, aspnet_Users user, out string msg);
        List<tsilurik_status> GetMailStatuses();
        #endregion
    }
}