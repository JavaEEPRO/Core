using arkAS.BLL;
using arkAS.Models;
            using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arkAS.Areas.Vasilyev.BLL
{
    public class MailManager : IMailManager
    {
        #region System

        public IRepository db;
        public IManager mng;
        private bool _disposed;

        public MailManager(IRepository db, IManager mng)
        {
            this.db = db;
            this.mng = mng;
            _disposed = false;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {

            }
            _disposed = true;
        }

        private void _debug(Exception ex, Object parameters = null, string additions = "")
        {
            RDL.Debug.LogError(ex, "", parameters);
        }
        #endregion

        #region Mails

        private IQueryable<vas_mails> _getMails()
        {
            return db.GetMails().OrderBy(x => x.date);
        }
        private bool _canAccessToMail(aspnet_Users user, vas_mails item)
        {
            var res = false;
            if ((user != null && user.UserName == "touch-test@gmail.com") && (item == null || item is vas_mails))
            {
                return true;
            }
            return res;
        }
        private bool _canManageMail(aspnet_Users user, vas_mails item = null)
        {
            var res = false;
            if ((user != null && user.UserName == "touch-test@gmail.com") && (item == null || item is vas_mails))
            {
                return true;
            }
            return res;
        }
        public bool _logMailStatusChange(vas_mails item, string note = "")
        {
            var res = false;
            try
            {
                db.SaveMailStatusLog(new vas_mailStatusesLog
                {
                    id = 0,
                    created = DateTime.Now,
                    statusID = item.statusID,
                    mailID = item.id,
                    note = note
                });
                res = true;
            }
            catch (Exception ex)
            {
                _debug(ex, new { documentID = item.id, statusID = item.statusID, note });
            }
            return res;
        }
        private string _getRandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public vas_mails GetMail(int id, aspnet_Users user, out string msg)
        {
            var res = new vas_mails();
            msg = "";
            try
            {
                res = db.GetMail(id);
                if (!_canAccessToMail(user, res))
                {
                    msg = "Нет прав для данной операции";
                    return res = null;
                }
            }
            catch (Exception ex)
            {
                _debug(ex, new { mailID = id, userName = user.UserName });
                res = null;
                msg = "Сбой при выполнеии операции";
            }

            return res;
        }

        public List<vas_mails> GetMails(ASCRUDGetItemsModel parameters, aspnet_Users user, out string msg)
        {
            msg = "";
            var res = new List<vas_mails>();

            if (!_canAccessToMail(user, null))
            {
                msg = "Нет прав для данной операции";
                return res = null;
            }

            try
            {
                var items = _getMails();
                if (parameters.filter != null && parameters.filter.Count > 0)
                {
                    var from = parameters.filter.ContainsKey("from") ? parameters.filter["from"].ToString() : "";
                    var to = parameters.filter.ContainsKey("to") ? parameters.filter["to"].ToString() : "";
                    List<int?> statusIDs = new List<int?>();
                    if (parameters.filter.ContainsKey("statusIDs"))
                    {
                        statusIDs = (parameters.filter["statusIDs"] as ArrayList).ToArray().Select(x => (int?)RDL.Convert.StrToInt(x.ToString(), 0)).ToList();
                    }
                    DateTime createdMin = DateTime.MinValue, createdMax = DateTime.MaxValue;
                    if (parameters.filter.ContainsKey("date") && parameters.filter["date"] != null)
                    {
                        var dates = parameters.filter["date"].ToString().Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                        if (dates.Length > 0)
                        {
                            createdMin = RDL.Convert.StrToDateTime(dates[0].Trim(), DateTime.MinValue);
                        }
                        if (dates.Length > 1)
                        {
                            createdMax = RDL.Convert.StrToDateTime(dates[1].Trim(), DateTime.MaxValue);
                        }
                    }

                    items = items.Where(x => (statusIDs.Count == 0 || statusIDs.Contains(x.statusID)) && (createdMin <= x.date && x.date <= createdMax));

                    if (from != "")
                    {
                        items = items.ToList().Where(x => x.from != null && x.from.IndexOf(from, StringComparison.CurrentCultureIgnoreCase) >= 0).AsQueryable();
                    }

                    if (to != "")
                    {
                        items = items.ToList().Where(x => x.to != null && x.to.IndexOf(to, StringComparison.CurrentCultureIgnoreCase) >= 0).AsQueryable();
                    }

                }

                var sorts = parameters.sort.Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries);
                var directions = parameters.direction.Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries);
                var sort1 = sorts.Length > 0 ? sorts[0] : "";
                var direction1 = directions.Length > 0 ? directions[0] : "";

                switch (sort1)
                {
                    case "from":
                        if (direction1 == "up") items = items.OrderBy(x => x.from);
                        else items = items.OrderByDescending(x => x.from);
                        break;
                    case "to":
                        if (direction1 == "up") items = items.OrderBy(x => x.to);
                        else items = items.OrderByDescending(x => x.to);
                        break;
                    case "date":
                        if (direction1 == "up") items = items.OrderBy(x => x.date);
                        else items = items.OrderByDescending(x => x.date);
                        break;
                    case "mailSystem":
                        if (direction1 == "up") items = items.OrderBy(x => x.mailSystem);
                        else items = items.OrderByDescending(x => x.mailSystem);
                        break;
                    case "dateReplay":
                        if (direction1 == "up") items = items.OrderBy(x => x.dateReplay);
                        else items = items.OrderByDescending(x => x.dateReplay);
                        break;
                    case "status":
                        if (direction1 == "up") items = items.OrderBy(x => x.vas_mailStatuses.name);
                        else items = items.OrderByDescending(x => x.vas_mailStatuses.name);
                        break;
                    default:
                        if (direction1 == "up") items = items.OrderBy(x => x.date);
                        else items = items.OrderByDescending(x => x.date);
                        break;
                }
                res = items.Skip(parameters.pageSize * (parameters.page - 1)).Take(parameters.pageSize).ToList();
            }

            catch (Exception ex)
            {
                _debug(ex, new { userName = user.UserName });
                res = null;
                msg = "Сбой при выполнеии операции";
            }

            return res;
        }

        public vas_mails CreateMail(string from, string to, aspnet_Users user, out string msg)
        {
            var res = new vas_mails();
            msg = "";
            try
            {
                if (!_canManageMail(user))
                {
                    msg = "Нет прав для данной операции";
                    return res = null;
                }

                res.from = from;
                res.to = to;
                res.trackNumber = _getRandomString(8); ;
                res.date = DateTime.Now;
                res.statusID = 1;
                res.mailSystem = "Default Mail System";
                res.comment = "Комментарий";
                res.code = _getRandomString(8).ToLower();

                db.SaveMail(res);

            }
            catch (Exception ex)
            {
                _debug(ex, new { from = from, to = to, userName = user.UserName });
                res = null;
                msg = "Сбой при выполнеии операции";
            }

            return res;
        }

        public bool DeleteMail(int id, aspnet_Users user, out string msg)
        {
            var res = false;
            msg = "";
            try
            {
                var item = db.GetMail(id);
                if (!_canManageMail(user, item))
                {
                    msg = "У вас нет прав на данную операцию";
                    return res;
                }
                if (!db.DeleteMail(id))
                {
                    throw new Exception("Сбой при удалении записи с базы");
                }
                res = true;
            }
            catch (Exception ex)
            {
                _debug(ex, new { mailID = id, userName = user.UserName });
                msg = "Сбой при выполнеии операции";
            }
            return res;
        }

        public bool EditMailField(int pk, string name, string value, aspnet_Users user, out string msg)
        {
            var res = false;
            msg = "";
            try
            {
                var item = db.GetMail(pk);
                if (!_canManageMail(user, item))
                {
                    msg = "Нет прав для данной операции";
                    return res;
                }

                switch (name)
                {
                    case "from": item.from = value; break;
                    case "to": item.to = value; break;
                    case "mailSystem": item.mailSystem = value; break;
                    case "comment": item.comment = value; break;
                    case "status": if (value != "") item.statusID = RDL.Convert.StrToInt(value, 0);
                        _logMailStatusChange(item, "Статус изменен " + user.UserName);
                        break;
                }
                db.SaveMail(item);
                res = true;
            }
            catch (Exception ex)
            {
                _debug(ex, new { mailID = pk, userName = user.UserName });
                msg = "Сбой при выполнеии операции";
            }
            return res;
        }

        #endregion

        #region Mail Statuses

        public vas_mailStatuses GetMailStatus(int id)
        {
            var res = new vas_mailStatuses();

            try
            {
                res = db.GetMailStatus(id);
            }
            catch (Exception ex)
            {
                _debug(ex);
            }

            return res;
        }

        public List<vas_mailStatuses> GetMailStatuses()
        {
            var res = new List<vas_mailStatuses>();

            try
            {
                res = db.GetMailStatuses().ToList();
            }
            catch (Exception ex)
            {
                _debug(ex);
            }
            return res;
        }

        public vas_mailStatuses CreateMailStatus(string name, string code)
        {
            var res = new vas_mailStatuses() { name = name, code = code };

            try
            {
                db.SaveMailStatus(res);
            }
            catch (Exception ex)
            {
                _debug(ex);
            }

            return res;
        }

        public void DeleteMailStatus(int id)
        {
            try
            {
                db.DeleteMailStatus(id);
            }
            catch (Exception ex)
            {
                _debug(ex);
            }
        }

        #endregion

    }
}