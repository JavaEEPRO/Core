using arkAS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arkAS.Areas.Mikhailova.BLL
{
    public interface IMailsManager : IDisposable
    {
        #region Mails

        List<Mikhailova_mails> GetMails();
        Mikhailova_mails GetMail(int id);
        void SaveMail(Mikhailova_mails item);
        bool CreateMail(int contractFromID, int contractToID);
        bool DeleteMail(int id, out string msg, Mikhailova_contracts user);

        #endregion

        #region StatusesMail

        List<Mikhailova_statuses_mails> GetStatusesMail();
        Mikhailova_statuses_mails GetStatusMail(int id);

        #endregion
    }
}
