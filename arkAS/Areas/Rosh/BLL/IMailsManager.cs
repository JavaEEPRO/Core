using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using arkAS.BLL;

namespace arkAS.Areas.Rosh.BLL
{
    public interface IMailsManager: IDisposable
    {
        #region System
        // void Save();
        #endregion

        #region mails
        List<rosh_mails> GetMails();
        rosh_mails GetMail(int id);
        void SaveMail(rosh_mails mail);
        void DeleteMail(int id);
        #endregion

        #region mailStatuses
        List<rosh_mailStatuses> GetMailStatuses();
        rosh_mailStatuses GetMailStatus(int id);
        void SaveMailStatus(rosh_mailStatuses mailStatus);
        void DeleteMailStatus(int id);
        #endregion

        #region mailLogs
        List<rosh_mailLogs> GetMailLogs();
        rosh_mailLogs GetMailLog(int id);
        void SaveMailLog(rosh_mailLogs mailLog);
        void DeleteMailLog(int id);
        #endregion

        #region sendingSystems
        List<rosh_sendingSystems> GetSendingSystems();
        rosh_sendingSystems GetSandingSystem(int id);
        void SaveSandingSystem(rosh_sendingSystems sandingSystem);
        void DeleteSandingSystem(int id);
        #endregion

    }
}
