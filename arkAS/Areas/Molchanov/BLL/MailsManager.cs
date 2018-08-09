using arkAS.Areas.Molchanov.Infrastructure;
using arkAS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arkAS.Areas.Molchanov.BLL
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
        private bool _canAccessToItem(aspnet_Users user)
        {
            var res = false;
            var isAdmin = user.aspnet_Roles.Any(x => x.RoleName == "admin");
            var isManager = user.aspnet_Roles.Any(x => x.RoleName == "guest");
            if (user != null && isAdmin || isManager)
            {
                res = true;
            }
            return res;
        }
        private bool _canManageItem(aspnet_Users user)
        {
            var res = false;
            var isAdmin = user.aspnet_Roles.Any(x => x.RoleName == "admin");
            if (user != null && isAdmin)
            {
                res = true;
            }
            return res;
        }
        private molchanov_logMails _logMailsChanges(aspnet_Users user, molchanov_mails element, string notice)
        {
            var res = new molchanov_logMails { date = DateTime.Now, userName = user.UserName, notice = "{element.uniqueCode.ToString()}: " + notice, mailID = element.id };
            return res;
        }
        private molchanov_logMailStatuses _logMailStatusesChanges(aspnet_Users user, molchanov_mailStatuses element, string notice)
        {
            var res = new molchanov_logMailStatuses { date = DateTime.Now, userName = user.UserName, notice = "{element.name}: " + notice, mailStatusID = element.id };
            return res;
        }
        private molchanov_logDeliverySystems _logDeliverySystemsChanges(aspnet_Users user, molchanov_deliverySystems element, string notice)
        {
            var res = new molchanov_logDeliverySystems { date = DateTime.Now, userName = user.UserName, notice = "{element.name}: " + notice, mailDeliverySystemID = element.id };
            return res;
        }
        #endregion
        #region Mails
        public List<molchanov_mails> GetMails(out string msg, aspnet_Users user)
        {
            msg = "";
            List<molchanov_mails> res;
            try
            {
                if (!_canAccessToItem(user))
                {
                    msg = "Нет прав на получение списка элемента!";
                    res = null;
                }
                else
                {
                    res = _db.GetMails().ToList();
                }
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при получении списка элемента");
                res = null;
            }
            return res;
        }

        public molchanov_mails GetMail(int id, out string msg, aspnet_Users user)
        {
            msg = "";
            molchanov_mails res;
            try
            {
                if (!_canAccessToItem(user))
                {
                    msg = "Нет прав на получение элемента по id";
                    res = null;
                }
                else
                {
                    res = _db.GetMail(id);

                }
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при получении элемента по id");
                res = null;
            }
            return res;
        }

        public molchanov_mails CreateMail(Dictionary<string, string> parameters, out string msg, aspnet_Users user)
        {
            msg = "";
            molchanov_mails res;
            try
            {
                if (!_canManageItem(user))
                {
                    msg = "Нет прав создавать элемента";
                    res = null;
                }
                else
                {
                    res = new molchanov_mails();
                    res.uniqueCode = Guid.NewGuid();
                    foreach (var key in parameters.Keys)
                    {
                        if (key == "date")
                        {
                            res.date = Convert.ToDateTime(parameters[key]);
                        }
                        else if (key == "fromSender")
                        {
                            res.fromSender = parameters[key];
                        }
                        else if (key == "toRecipient")
                        {
                            res.toRecipient = parameters[key];
                        }
                        else if (key == "description")
                        {
                            res.description = parameters[key];
                        }
                        else if (key == "trackNumber")
                        {
                            res.trackNumber = parameters[key];
                        }
                        else if (key == "backTrackNumber")
                        {
                            res.backTrackNumber = parameters[key];
                        }
                        else if (key == "backDateReceipt")
                        {
                            res.date = Convert.ToDateTime(parameters[key]);
                        }
                        else if (key == "delSystemsName")
                        {
                            res.deliverySystemID = RDL.Convert.StrToInt(parameters[key], 0);
                        }
                        else if (key == "mailStatuses")
                        {
                            res.mailStatusID = RDL.Convert.StrToInt(parameters[key], 0);
                        }
                    }
                    _db.SaveMail(res);
                    _db.SaveMailLog(_logMailsChanges(user, res, "Элемент создан"));
                }
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при создании элемента");
                res = null;
            }
            return res;
        }

        public molchanov_mails EditMail(Dictionary<string, string> parameters, int id, out string msg, aspnet_Users user)
        {
            molchanov_mails res;
            msg = "";
            try
            {
                if (!_canManageItem(user))
                {
                    msg = "Нет прав редактировать элемента";
                    res = null;
                }
                else
                {
                    res = _db.GetMail(id);
                    foreach (var key in parameters.Keys)
                    {
                        if (key == "date")
                        {
                            res.date = Convert.ToDateTime(parameters[key]);
                        }
                        else if (key == "fromSender")
                        {
                            res.fromSender = parameters[key];
                        }
                        else if (key == "toRecipient")
                        {
                            res.toRecipient = parameters[key];
                        }
                        else if (key == "description")
                        {
                            res.description = parameters[key];
                        }
                        else if (key == "trackNumber")
                        {
                            res.trackNumber = parameters[key];
                        }
                        else if (key == "backTrackNumber")
                        {
                            res.backTrackNumber = parameters[key];
                        }
                        else if (key == "backDateReceipt")
                        {
                            res.backDateReceipt = Convert.ToDateTime(parameters[key]);
                        }
                        else if (key == "delSystemsName")
                        {
                            res.deliverySystemID = RDL.Convert.StrToInt(parameters[key], 0);
                        }
                        else if (key == "mailStatuses")
                        {
                            res.mailStatusID = RDL.Convert.StrToInt(parameters[key], 0);
                        }
                    }
                    _db.SaveMail(res);
                    _db.SaveMailLog(_logMailsChanges(user, res, "Элемент  отредактирован"));
                }
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при изменении элемента");
                res = null;
            }
            return res;
        }



        public bool ChangeMailsStatus(int id, string name, string value, out string msg, aspnet_Users user)
        {
            bool res;
            msg = "";
            try
            {
                if (!_canManageItem(user))
                {
                    msg = "Нет прав редактировать элемент";
                    res = false;
                }
                else
                {
                    var item = _db.GetMail(id);
                    switch (name)
                    {
                        case "delSystemsName":
                            int delID = RDL.Convert.StrToInt(value, 0);
                            item.deliverySystemID = delID;
                            _db.SaveMail(item);
                            _db.SaveMailLog(_logMailsChanges(user, item, "Статус изменен. Новый статус: {item.molchanov_deliverySystems.name}"));
                            res = true;
                            break;
                        case "mailStatuses":
                            int stID = RDL.Convert.StrToInt(value, 0);
                            item.mailStatusID = stID;
                            _db.SaveMail(item);
                            _db.SaveMailLog(_logMailsChanges(user, item, "Статус изменен. Новый статус: {item.molchanov_mailStatuses.name}"));
                            res = true;
                            break;
                        default:
                            res = false;
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при изменении элемента");
                res = false;
            }
            return res;
        }


        public bool RemoveMail(int id, out string msg, aspnet_Users user)
        {
            msg = "";
            bool res;
            try
            {
                if (!_canManageItem(user))
                {
                    msg = "Нет прав на удаление элемента";
                    res = false;
                }
                else
                {
                    _db.DeleteMail(id);
                    res = true;
                }
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при удалении элемента");
                res = false;
            }
            return res;
        }
        public List<molchanov_logMails> GetMailLogsByID(int id)
        {
            List<molchanov_logMails> res;
            try
            {
                res = _db.GetMailLogs().Where(x => x.mailID == id).ToList();
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка");
                res = null;
            }
            return res;
        }
        #endregion
        #region MailsStatuses
        public List<molchanov_mailStatuses> GetMailStatuses(out string msg, aspnet_Users user)
        {
            msg = "";
            List<molchanov_mailStatuses> res;
            try
            {
                if (!_canAccessToItem(user))
                {
                    msg = "Нет прав на получение списка элемента!";
                    res = null;
                }
                else
                {
                    res = _db.GetMailStatuses().ToList();
                }


            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при получении списка элемента");
                res = null;
            }
            return res;
        }

        public molchanov_mailStatuses GetMailStatus(int id, out string msg, aspnet_Users user)
        {
            msg = "";
            molchanov_mailStatuses res;
            try
            {
                if (!_canAccessToItem(user))
                {
                    msg = "Нет прав на получение элемента по id";
                    res = null;
                }
                else
                {
                    res = _db.GetMailStatus(id);

                }
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при получении элемента по id");
                res = null;
            }
            return res;
        }

        public molchanov_mailStatuses CreateMailStatus(Dictionary<string, string> parameters, out string msg, aspnet_Users user)
        {
            msg = "";
            molchanov_mailStatuses res;

            try
            {
                if (!_canManageItem(user))
                {
                    msg = "Нет прав создавать элемента";
                    res = null;
                }
                else
                {
                    res = new molchanov_mailStatuses();
                    foreach (var key in parameters.Keys)
                    {
                        if (key == "name")
                        {
                            res.name = parameters[key];
                        }
                        else if (key == "code")
                        {
                            res.code = parameters[key];
                        }
                    }
                    _db.SaveMailStatus(res);
                    _db.SaveMailStatusLog(_logMailStatusesChanges(user, res, "Статус почты создан"));
                }
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при создании элемента");
                res = null;
            }
            return res;
        }

        public molchanov_mailStatuses EditMailStatus(Dictionary<string, string> parameters, int id, out string msg, aspnet_Users user)
        {
            molchanov_mailStatuses res;
            msg = "";
            try
            {
                if (!_canManageItem(user))
                {
                    msg = "Нет прав редактировать элемента";
                    res = null;
                }
                else
                {
                    res = _db.GetMailStatus(id);
                    foreach (var key in parameters.Keys)
                    {
                        if (key == "name")
                        {
                            res.name = parameters[key];
                        }
                        else if (key == "code")
                        {
                            res.code = parameters[key];
                        }
                    }
                    _db.SaveMailStatus(res);
                    _db.SaveMailStatusLog(_logMailStatusesChanges(user, res, "Статус почты  изменен"));
                }
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при изменении элемента");
                res = null;
            }
            return res;
        }

        public bool RemoveMailStatus(int id, out string msg, aspnet_Users user)
        {
            msg = "";
            bool res;
            try
            {
                if (!_canManageItem(user))
                {
                    msg = "Нет прав на удаление элемента";
                    res = false;
                }
                else
                {
                    _db.DeleteMailStatus(id);
                    res = true;
                }
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при удалении элемента");
                res = false;
            }
            return res;
        }
        #endregion
        #region Delivery Systems
        public List<molchanov_deliverySystems> GetDeliverySystems(out string msg, aspnet_Users user)
        {
            msg = "";
            List<molchanov_deliverySystems> res;
            try
            {
                if (!_canAccessToItem(user))
                {
                    msg = "Нет прав на получение списка элемента!";
                    res = null;
                }
                else
                {
                    res = _db.GetDeliverySystems().ToList();
                }
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при получении списка элемента");
                res = null;
            }
            return res;
        }

        public molchanov_deliverySystems GetDeliverySystem(int id, out string msg, aspnet_Users user)
        {
            msg = "";
            molchanov_deliverySystems res;
            try
            {
                if (!_canAccessToItem(user))
                {
                    msg = "Нет прав на получение элемента по id";
                    res = null;
                }
                else
                {
                    res = _db.GetDeliverySystem(id);

                }
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при получении элемента по id");
                res = null;
            }
            return res;
        }

        public molchanov_deliverySystems CreateDeliverySystem(Dictionary<string, string> parameters, out string msg, aspnet_Users user)
        {
            msg = "";
            molchanov_deliverySystems res;

            try
            {
                if (!_canManageItem(user))
                {
                    msg = "Нет прав создавать элемента";
                    res = null;
                }
                else
                {
                    res = new molchanov_deliverySystems();
                    foreach (var key in parameters.Keys)
                    {
                        if (key == "name")
                        {
                            res.name = parameters[key];
                        }
                        else if (key == "code")
                        {
                            res.code = parameters[key];
                        }
                    }
                    _db.SaveDeliverySystem(res);
                    _db.SaveDeliverySystemLog(_logDeliverySystemsChanges(user, res, "Почтовая система создана"));
                }
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при создании элемента");
                res = null;
            }
            return res;
        }

        public molchanov_deliverySystems EditDeliverySystem(Dictionary<string, string> parameters, int id, out string msg, aspnet_Users user)
        {
            molchanov_deliverySystems res;
            msg = "";
            try
            {
                if (!_canManageItem(user))
                {
                    msg = "Нет прав редактировать элемента";
                    res = null;
                }
                else
                {
                    res = _db.GetDeliverySystem(id);
                    foreach (var key in parameters.Keys)
                    {
                        if (key == "name")
                        {
                            res.name = parameters[key];
                        }
                        else if (key == "code")
                        {
                            res.code = parameters[key];
                        }
                    }
                    _db.SaveDeliverySystem(res);
                    _db.SaveDeliverySystemLog(_logDeliverySystemsChanges(user, res, "Почтовая система изменена"));
                }
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при изменении элемента");
                res = null;
            }
            return res;
        }

        public bool RemoveDeliverySystem(int id, out string msg, aspnet_Users user)
        {
            msg = "";
            bool res;
            try
            {
                if (!_canManageItem(user))
                {
                    msg = "Нет прав на удаление элемента";
                    res = false;
                }
                else
                {
                    _db.DeleteDeliverySystem(id);
                    res = true;
                }
            }
            catch (Exception e)
            {
                _debug(e, new { }, "Ошибка возникла при удалении элемента");
                res = false;
            }
            return res;
        }
        #endregion

    }
}