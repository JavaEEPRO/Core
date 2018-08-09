using arkAS.BLL;
using arkAS.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace arkAS.Areas.Tsilurik.BLL
{
    internal class MailManager : IMailManager
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

        #region Mail

        private IQueryable<tsilurik_mails> _getMails()
        {
            return db.GetMails().OrderBy(x => x.date);
        }
        private bool _canAccessToMail(aspnet_Users user, tsilurik_mails item)
        {
            var res = false;
            if ((user != null && user.UserName == "touch-test@gmail.com") && (item == null || item is tsilurik_mails))
            {
                return true;
            }
            return res;
        }
        private bool _canManageMail(aspnet_Users user, tsilurik_mails item)
        {
            var res = false;
            if ((user != null && user.UserName == "touch-test@gmail.com") && (item == null || item is tsilurik_mails))
            {
                return true;
            }
            return res;
        }
        public bool _logMailStatuses(tsilurik_mails item, string note = "")
        {
            var res = false;
            try
            {
                db.SaveLogsDocumentStatus(new tsilurik_statusLog
                {

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

        public List<tsilurik_mails> GetMails(ASCRUDGetItemsModel parameters, aspnet_Users user, out string msg,out int total)
        {
            msg = "";
            total = 0;
            var res = new List<tsilurik_mails>();

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
                    var number = parameters.filter.ContainsKey("trackNumber") ? parameters.filter["trackNumber"].ToString() : "";
                    var from = parameters.filter.ContainsKey("from") ? parameters.filter["from"].ToString() : "";
                    var to = parameters.filter.ContainsKey("to") ? parameters.filter["to"].ToString() : "";
                    List<int?> statusIDs = new List<int?>();
                    if (parameters.filter.ContainsKey("statusIDs"))
                    {
                        statusIDs = (parameters.filter["statusIDs"] as ArrayList).ToArray().Select(x => (int?)RDL.Convert.StrToInt(x.ToString(), 0)).ToList();
                    }


                    items = items.Where(x => (statusIDs.Count == 0 || statusIDs.Contains(x.statusID)) );

                    if (number != "")
                    {
                        items = items.ToList().Where(x => x.trackNumber != null && x.trackNumber.IndexOf(number, StringComparison.CurrentCultureIgnoreCase) >= 0).AsQueryable();
                    }
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
                    case "returnDate":
                        if (direction1 == "up") items = items.OrderBy(x => x.returnDate);
                        else items = items.OrderByDescending(x => x.returnDate);
                        break;
                    case "status":
                        if (direction1 == "up") items = items.OrderBy(x => x.tsilurik_status.name);
                        else items = items.OrderByDescending(x => x.tsilurik_status.name);
                        break;
                    default:
                        if (direction1 == "up") items = items.OrderBy(x => x.date);
                        else items = items.OrderByDescending(x => x.date);
                        break;
                }
                total = items.Count();
                res = items.Skip(parameters.pageSize * (parameters.page - 1)).Take(parameters.pageSize).ToList();
            }

            catch (Exception ex)
            {
                _debug(ex, new { userName = user.UserName });
                res = null;
                msg = "Сбой при выполнении операции";
            }

            return res;
        }
        public tsilurik_mails GetMail(int id, aspnet_Users user, out string msg)
        {
            var res = new tsilurik_mails();
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
                msg = "Сбой при выполнении операции";
            }

            return res;
        }
        public tsilurik_mails CreateMail(Dictionary<string, object> parameters, aspnet_Users user, out string msg)
        {
            var res = new tsilurik_mails();
            msg = "";
            try
            {
                if (!_canManageMail(user, res))
                {
                    msg = "Нет прав для данной операции";
                    return res = null;
                }
                var fields = (parameters["fields"] as ArrayList).ToArray().ToList().Select(x => x as Dictionary<string, object>).ToList();
                if (RDL.Convert.StrToInt(AjaxModel.GetValueFromSaveField("id", fields), 0) == 0)
                {
                    var date = DateTime.Now;
                    var from = AjaxModel.GetValueFromSaveField("from", fields);
                    var to = AjaxModel.GetValueFromSaveField("to", fields);
                    var trackNumber = AjaxModel.GetValueFromSaveField("trackNumber", fields);
                    var mailSystem = AjaxModel.GetValueFromSaveField("mailSystem", fields);
                    var returnTrackNumber = AjaxModel.GetValueFromSaveField("returnTrackNumber", fields);
                    var returnDate = RDL.Convert.StrToDateTime(AjaxModel.GetValueFromSaveField("returnDate", fields), DateTime.Now);
                    var description = AjaxModel.GetValueFromSaveField("description", fields);
                    var statusID = GetMailStatuses().FirstOrDefault(x => x.name == "создано").id;
                    var item = new tsilurik_mails { trackNumber = trackNumber, date = date, from = from, to = to, statusID = statusID,
                        mailSystem = mailSystem, returnTrackNumber = returnTrackNumber, returnDate = returnDate, description=description };
                    db.SaveMail(item);
                    res = item;
                }
                else
                {
                    res = _editMail(fields, out msg);
                }

            }
            catch (Exception ex)
            {
                _debug(ex, new { userName = user.UserName });
                res = null;
                msg = "Сбой при выполнении операции";
            }

            return res;
        }
        public bool ChangeMailStatus(int id, string statusID, string name, aspnet_Users user, out string msg)
        {
            var res = false;
            msg = "";
            try
            {
                var item = db.GetMail(id);
                if (!_canManageMail(user, item))
                {
                    msg = "Нет прав для данной операции";
                    return res;
                }

                switch (name)
                {
                    case "status":
                        if (statusID != "") item.statusID = RDL.Convert.StrToInt(statusID, 0);
                        _logMailStatuses(item, "Статус изменен " + user.UserName);
                        break;
                }
                db.SaveMail(item);
                res = true;
            }
            catch (Exception ex)
            {
                _debug(ex, new { mailID = id });
                res = false;
                msg = "Сбой при выполнении операции";
            }

            return res;
        }
        public tsilurik_mails _editMail(List<Dictionary<string, object>> fields, out string msg)
        {
            var res = new tsilurik_mails();
            msg = "";
            try
            {
                var id = RDL.Convert.StrToInt(AjaxModel.GetValueFromSaveField("id", fields), 0);
                var item = db.GetMail(id);

                item.from = AjaxModel.GetValueFromSaveField("from", fields);
                item.to = AjaxModel.GetValueFromSaveField("to", fields);
                item.trackNumber = AjaxModel.GetValueFromSaveField("trackNumber", fields);
                item.mailSystem = AjaxModel.GetValueFromSaveField("mailSystem", fields);
                item.returnTrackNumber = AjaxModel.GetValueFromSaveField("returnTrackNumber", fields);
                item.returnDate = RDL.Convert.StrToDateTime(AjaxModel.GetValueFromSaveField("returnDate", fields), DateTime.Now);
                item.description = AjaxModel.GetValueFromSaveField("description", fields);
                db.SaveMail(item);
                res = item;
            }
            catch (Exception ex)
            {
                _debug(ex, new { mailID = res.id });
                res = null;
                msg = "Сбой при выполнении операции";
            }

            return res;
        }
        public List<tsilurik_status> GetMailStatuses()
        {
            var res = new List<tsilurik_status>();
            try
            {
                res = db.GetStatuses("mail").ToList();
            }
            catch (Exception ex)
            {
                _debug(ex);
                res = null;
            }
            return res;
        }

        #endregion
    }
}