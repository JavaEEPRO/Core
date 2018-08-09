using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using arkAS.BLL;

namespace arkAS.Areas.Udovika.BLL
{
    public interface IMailsManager
    {
        #region Mails
        List<udovika_email> GetMails();
        udovika_email GetMail(int id, aspnet_Users user, out string msg);
        bool CreateMail(string from, string to, aspnet_Users user, out string msg);
        int SaveMail(udovika_email mail, aspnet_Users user, out string msg);
        bool DeleteMail(int id, aspnet_Users user, out string msg);
        bool EditMailField(int id, string name, string value, out string msg, aspnet_Users user);
        #endregion
        #region Status
        List<udovika_statusEmail> GetMailStatuses();
        udovika_statusEmail GetMailStatus(int id);
        #endregion
    }
}
