using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using arkAS.BLL;
using arkAS.Models;
using arkAS.BLL.Core;
using Dapper;
using System.Data;
using arkAS.Areas.Motskin.Infrastructura;

namespace arkAS.Areas.Motskin.BLL
{
    public class MailManager: IMailManager
    {

        #region System

        private IRepository _db;
        private IManager _mng;
        private bool _disposed;

        public MailManager(IRepository db, IManager mng)
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
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_db != null)
                        _db.Dispose();
                }
                _db = null;
                _disposed = true;
            }
        }

        private void _debug(Exception ex, Object parameters = null, string additions = "")
        {
            RDL.Debug.LogError(ex, additions, parameters);
        }

        private bool _IsCanUserChange(aspnet_Users user)
        {
            return (user != null && user.UserName == StaticData.User);
        }

        public bool IsCanCurrentUserChange()
        {
            return _IsCanUserChange(_mng.GetUser());
        }
        #endregion

        #region Mail

        public dynamic GetItems(ASCRUDGetItemsModel parameters, aspnet_Users user, out string msg, out int total)
        {
            msg = "";
            total = 0;
            var fromAddress = "";
            var toAddress = "";
            var sendSystemID = "";
            var strStatusIDs = "";
            DateTime createdMin = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
            DateTime createdMax = (DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue;

            if (!_IsCanUserChange(user))
            {
                msg = "Нет прав для данной операции";
                return null;
            }
            try
            {
                if (parameters.filter != null && parameters.filter.Count > 0)
                {
                    fromAddress = parameters.filter.ContainsKey("fromAddress") ? parameters.filter["fromAddress"].ToString() : "";
                    toAddress = parameters.filter.ContainsKey("toAddress") ? parameters.filter["toAddress"].ToString() : "";
                    sendSystemID = parameters.filter.ContainsKey("sendSystemID") ? parameters.filter["sendSystemID"].ToString() : "";
                    strStatusIDs = parameters.filter.ContainsKey("strStatusIDs") ? parameters.filter["strStatusIDs"].ToString() : "";
                    if (parameters.filter.ContainsKey("created") && parameters.filter["created"] != null)
                    {
                        var dates = parameters.filter["created"].ToString().Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                        if (dates.Length > 0)
                        {
                            createdMin = RDL.Convert.StrToDateTime(dates[0].Trim(), (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue);
                        }
                        if (dates.Length > 1)
                        {
                            createdMax = RDL.Convert.StrToDateTime(dates[1].Trim(), (DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue);
                            createdMax = createdMax.AddDays(1).AddSeconds(-1);
                        }
                    }
                }

                var sorts = parameters.sort.Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries);
                var directions = parameters.direction.Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries);
                var sort1 = sorts.Length > 0 ? sorts[0] : "";
                var direction1 = directions.Length > 0 ? directions[0] : "";

                var rep = new CoreRepository();
                var p = new DynamicParameters();
                p.Add("fromAddress", fromAddress);
                p.Add("toAddress", toAddress);
                p.Add("sendSystemID", sendSystemID);
                p.Add("strStatusIDs", strStatusIDs);
                p.Add("createdMin", createdMin);
                p.Add("createdMax", createdMax);
                p.Add("sort1", sort1);
                p.Add("direction1", direction1);
                p.Add("page", parameters.page);
                p.Add("pageSize", parameters.pageSize);
                p.Add("total", dbType: DbType.Int32, direction: ParameterDirection.Output);
                dynamic res = rep.GetSQLData<dynamic>("motskin_GetMails", p, CommandType.StoredProcedure) as List<dynamic> ?? new List<dynamic>();

                total = p.Get<int>("total");
                return res;
            }

            catch (Exception ex)
            {
                _debug(ex, new { userName = user.UserName });
                msg = "Сбой при выполнении операции";
                return null;
            }
        }

        public motskin_mails Get(int id, aspnet_Users user, out string msg)
        {
            motskin_mails res;
            msg = "";
            try
            {
                if (!_IsCanUserChange(user))
                {
                    msg = "Нет прав для данной операции";
                    return null;
                }
                res = _db.GetMails().FirstOrDefault(x => x.id == id);

            }
            catch (Exception ex)
            {
                _debug(ex, new { mailID = id, userName = user.UserName });
                res = null;
                msg = "Сбой при выполнении операции";
            }

            return res;
        }

        public motskin_mails Create(Dictionary<string, string> parameters, aspnet_Users user, out string msg)
        {
            motskin_mails res=null;
            msg = "";
            try
            {
                if (!_IsCanUserChange(user))
                {
                    msg = "Нет прав для данной операции";
                    return null;
                }
                var statusID = GetStatuses().FirstOrDefault(x => x.code == "created").id;
                Guid guid = Guid.NewGuid();
                res = new motskin_mails
                {
                    date = DateTime.Now,        // дата неизменна
                    mailStatusID = statusID,
                    createdUnique = guid
                };
                _fillMail(res, parameters);

                _db.SaveMail(res);  // сохраняем в бд
                                    // и сразу же получаем из базы этот объект, чтобы узнать его id.
                motskin_mails item = _db.GetMails().FirstOrDefault(x => x.createdUnique == guid);
                _logChangeStatus(item, "Статус изменен " + user.UserName);
            }
            catch (Exception ex)
            {
                _debug(ex, new { userName = user.UserName });
                msg = "Сбой при выполнении операции";
            }

            return res;
        }

        public motskin_mails Edit(Dictionary<string, string> parameters, aspnet_Users user, out string msg)
        {
            motskin_mails res;
            msg = "";
            try
            {
                if (!_IsCanUserChange(user))
                {
                    msg = "Нет прав для данной операции";
                    return null;
                }

                int id = int.Parse(parameters["id"]);

                res = _db.GetMails().FirstOrDefault(x => x.id == id);
                if (res != null)
                {
                    _fillMail(res, parameters);
                    _db.SaveMail(res);  // сохраняем в бд
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

        private void _fillMail(motskin_mails item, Dictionary<string, string> parameters)
        {
            foreach (var key in parameters.Keys)
            {
                switch (key)
                {
                    case "fromAddress":
                        item.fromAddress = parameters[key];
                        break;
                    case "toAddress":
                        item.toAddress = parameters[key];
                        break;
                    case "trackNumber":
                        item.trackNumber = parameters[key];
                        break;
                    case "backTrackNumber":
                        item.backTrackNumber = parameters[key];
                        break;
                    case "description":
                        item.description = parameters[key];
                        break;
                    case "sendSystemName":
                        item.sendSystemID = RDL.Convert.StrToInt(parameters[key], 0);
                        break;
                    case "backDateReceived":
                        if (parameters[key]!="")
                            item.backDateReceived = RDL.Convert.StrToDateTime(parameters[key], new DateTime());
                        else
                            item.backDateReceived = null; 
                        break;
                }
            }
        }

        public bool Delete(int id, aspnet_Users user, out string msg)
        {
            bool res = true;
            msg = "";
            try
            {
                if (!_IsCanUserChange(user))
                {
                    msg = "Нет прав для данной операции";
                    return res;
                }
                RDL.CacheManager.PurgeCacheItems("motskin_mails");
                res = _db.DeleteMail(id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { mailID = id });
                res = false;
                msg = "Сбой при выполнении операции";
            }

            return res;
        }

        public List<motskin_sendSystems> GetSystems()
        {
            var res = new List<motskin_sendSystems>();
            try
            {
                res = _db.GetSendSystems().ToList();
            }
            catch (Exception ex)
            {
                _debug(ex);
                res = null;
            }

            return res;
        }
        #endregion

        #region Status

        public bool ChangeStatus(int id, string value, string parametrName, aspnet_Users user, out string msg)
        {
            var res = false;
            msg = "";
            try
            {
                if (!_IsCanUserChange(user))
                {
                    msg = "Нет прав для данной операции";
                    return res;
                }

                var item = _db.GetMails().FirstOrDefault(x => x.id == id);

                switch (parametrName)
                {
                    case "mailStatusName":
                        int statusID = 0;
                        if (value != "")
                            statusID = RDL.Convert.StrToInt(value, 0);
                        if (item.mailStatusID != statusID)  // проверка, если этот метод вызовет контроллер напрямую
                        {
                            item.mailStatusID = statusID;
                            _logChangeStatus(item, "Статус изменен " + user.UserName);
                            _db.SaveMail(item);
                        }
                        res = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                _debug(ex, new { mailID = id });
                res = false;
                msg = "Сбой при выполнении операции";
            }

            return res;
        }

        public List<motskin_mailStatuses> GetStatuses()
        {
            var res = new List<motskin_mailStatuses>();
            try
            {
                res = _db.GetMailStatuses().ToList();
            }
            catch (Exception ex)
            {
                _debug(ex);
                res = null;
            }
            return res;
        }
        #endregion

        #region LogStatus
        public List<motskin_mailStatusLog> GetLogStatus(int id)
        {
            var res = new List<motskin_mailStatusLog>();
            try
            {
                res = _db.GetMailStatusLog().Where(x => x.mailID == id).ToList();
            }
            catch (Exception ex)
            {
                _debug(ex);
                res = null;
            }

            return res;
        }

        private bool _logChangeStatus(motskin_mails item, string note = "")
        {
            var res = false;
            try
            {
                _db.SaveMailStatusLog(new motskin_mailStatusLog
                {
                    created = DateTime.Now,
                    mailStatusID = item.mailStatusID,
                    mailID = item.id,
                    note = note
                });
                res = true;
            }
            catch (Exception ex)
            {
                _debug(ex, new { mailID = item.id, statusID = item.mailStatusID, note });
            }
            return res;
        }
#endregion

    }
}