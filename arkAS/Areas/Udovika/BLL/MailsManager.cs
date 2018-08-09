using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arkAS.BLL;

namespace arkAS.Areas.Udovika.BLL
{
    public class MailsManager : IMailsManager
    {
        #region System
        public IRepository db;
        public IManager mng;
        private bool disposed;

        public MailsManager(IRepository db, IManager mng)
        {
            this.db = db;
            this.mng = mng;
            disposed = false;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }
        private void _debug(Exception ex, Object parameters, string additions = "")
        {
            RDL.Debug.LogError(ex, additions, parameters);
        }
        #endregion
        #region Mail
        public bool GetPermissionAccessMail(aspnet_Users user)
        {
            var res = false;
            if (user != null && user.UserName == "core-guest@mail.ru")
            {
                return true;
            }
            return res;
        }
        public List<udovika_email> GetMails()
        {
            var res = new List<udovika_email>();
            try
            {
                res = db.GetMails().ToList();
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
        }
        public udovika_email GetMail(int id, aspnet_Users user, out string msg)
        {
            msg = "";
            var item = new udovika_email();
            try
            {
                if (!GetPermissionAccessMail(user))
                {
                    msg = "Недостаточно прав.";
                    return item = null;
                }
                item = GetMails().FirstOrDefault(x => x.id == id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { id = id }, "");   
            }
            return item;
        }
        public bool CreateMail(string from, string to, aspnet_Users user, out string msg)
        {
            bool res = false;
            var mail = new udovika_email();
            msg = "";
            try
            {
                if (!GetPermissionAccessMail(user))
                {
                    msg = "Недостаточно прав.";
                    return res;
                }
                else
                {
                    mail = new udovika_email()
                    {
                        id = 0,
                        from = from,
                        to = to,
                        date = DateTime.Now
                    };
                    SaveMail(mail, user, out msg);
                    res = true;
                    msg = "Сообщение создано.";
                }
            }
            catch (Exception ex)
            {
                _debug(ex, mail, "");
                msg = "Не удалось создать сообщение.";
            }
            return res;
        }
        public int SaveMail(udovika_email mail, aspnet_Users user, out string msg)
        {
            msg = "";
            try
            {
                if (!GetPermissionAccessMail(user))
                {
                    msg = "Недостаточно прав.";
                    return 0;
                }
                else
                    db.SaveMail(mail);
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return mail.id;
        }
        public bool DeleteMail(int id, aspnet_Users user, out string msg)
        {
            bool res = false;
            var item = new udovika_email();
            try
            {

                if (!GetPermissionAccessMail(user))
                {
                    msg = "Недостаточно прав.";
                    return res;
                }
                else
                {
                    db.DeleteMail(id);
                    res = true;
                    msg = "Сообщение удалено.";
                }
                
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
                msg = "Ошибка. Сообщение не удалено.";
            }
            return res;
        }
        public bool EditMailField(int id, string name, 
            string value, out string msg, aspnet_Users user)
        {
            var res = false;
            msg = "";
            try
            {
                var item = GetMail(id, user, out msg);
                if (item==null)
                {
                    return res;
                }
                switch (name)
                {
                    case "from": 
                        item.from = value; 
                        break;
                    case "to": 
                        item.to = value; 
                        break;
                    case "description": 
                        item.description = value; 
                        break;
                    case "status":  
                        item.statusID = RDL.Convert.StrToInt(value, 0);
                        break;
                    case "trackNum":
                        item.trackNum = value;
                        break;
                    case "system":
                        item.system = value;
                        break;
                    case "backNum":
                        item.backNum = value;
                        break;
                    case "backDate":
                        item.backDate = RDL.Convert.StrToDateTime(value, DateTime.Now.Date);
                        break;
                }
                SaveMail(item, user, out msg);
                res = true;
                msg = "Изменено.";
            }
            catch (Exception ex)
            {
                _debug(ex, new { mailID = id });
                msg = "Ошибка.";
            }
            return res;
        }
        #endregion
        #region MailStatus
        public List<udovika_statusEmail> GetMailStatuses()
        {
            var res = new List<udovika_statusEmail>();
            try
            {
                res = db.GetMailStatuses().ToList();
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
        }
        public udovika_statusEmail GetMailStatus(int id)
        {
            var res = new udovika_statusEmail();
            try
            {
                res = GetMailStatuses().FirstOrDefault(x => x.id == id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { StatusID = id }, "");
            }
            return res;
        }
        #endregion
    }
}
