using arkAS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arkAS.Areas.Shtoda.BLL
{
    public interface IMailsManager : IDisposable
    {
        #region Mails

        List<Shtoda_mails> GetMails();
        Shtoda_mails GetMail(int id);
        void SaveMail(Shtoda_mails item);
        bool CreateMail(int contractFromID, int contractToID);
        bool DeleteMail(int id, out string msg, Shtoda_contracts user);

        #endregion

        #region StatusesMail

        List<Shtoda_statuses_mails> GetStatusesMail();
        Shtoda_statuses_mails GetStatusMail(int id);

        #endregion
    }
}
