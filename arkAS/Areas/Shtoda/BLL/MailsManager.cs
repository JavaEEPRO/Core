using arkAS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arkAS.Areas.Shtoda.BLL
{
    public class MailsManager : IMailsManager
    {
        #region System 
        private IRepository _db;
        private IManager _mng;
        private bool _disposed;

        public MailsManager(IRepository db, IManager mng)
        {
            _db = db;
            _mng = mng;
            _disposed = false;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(!_disposed)
            {
                if(disposing)
                {
                    if (_db != null)
                        _db.Dispose();
                }
                _db = null;
                _disposed = true;
            }
        }

        private void _debug(Exception ex, Object parameters, string additions = "")
        {
            RDL.Debug.LogError(ex, additions, parameters);
        }

        #endregion 

        #region Mails

        public List<Shtoda_mails> GetMails()
        {
            var res = new List<Shtoda_mails>();
            try
            {
                res = _db.GetMails().ToList();
            }
            catch (Exception ex)
            {
                _debug(ex, new object { }, "");
            }
            return res;
        }

        public Shtoda_mails GetMail(int id)
        {
            var res = new Shtoda_mails();
            try
            {
                res = _db.GetMails().FirstOrDefault(x => x.id == id);
            }
            catch (Exception ex)
            {
                _debug(ex, new object { }, "");
            }
            return res;
        }

        public void SaveMail(Shtoda_mails item)
        {
            try
            {
                _db.SaveMail(item);
            }
            catch (Exception ex)
            {
                _debug(ex, new object { }, "");
            }
        }

        public bool CreateMail(int contractorFromID, int contractorToID)
        {
            bool res = false;
            var mail = new Shtoda_mails();
            try
            {
               
                mail = new Shtoda_mails
                {
                    id = 0,
                    date = DateTime.Now,
                    from = contractorFromID.ToString(),
                    to = contractorToID.ToString()
                };
                SaveMail(mail);
            }
            catch (Exception ex)
            {
                _debug(ex, new object { }, "");
            }
            return res;
        }

        public bool DeleteMail(int id, out string msg, Shtoda_contracts user)
        {
            bool res = false;
            var mail = new Shtoda_mails();
            try
            {
                mail = GetMail(id);
                if (mail != null)
                {
                    if (mail.id != user.id) //
                    {
                        msg = "Нет прав на удаление письма";
                        return res;
                    }
                    //mail.isDeleted = true; insert in db
                    SaveMail(mail);
                    res = true;
                    msg = "Письмо удалено";
                }
                else
                    msg = "Не удалось найти письмо";
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
                msg = "Ошибка удаления письма";
            }
            return res;
        }

        #endregion

        #region StatusesMail

        public List<Shtoda_statuses_mails> GetStatusesMail()
        {
            var res = new List<Shtoda_statuses_mails>();
            try
            {
                res = _db.GetStatusesMail().ToList();
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
        }

        public Shtoda_statuses_mails GetStatusMail(int id)
        {
            var res = new Shtoda_statuses_mails();
            try
            {
                res = _db.GetStatusesMail().FirstOrDefault(x => x.id == id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
        }

        #endregion

    }
}