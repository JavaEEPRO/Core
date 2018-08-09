using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arkAS.BLL;

namespace arkAS.Areas.Podvashetskyi.BLL
{
    public class MailManager : IMailManager
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
        public List<pdv_mails> GetMails()
        {
            var res = new List<pdv_mails>();
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
        public pdv_mails GetMail(int id)
        {
            var res = new pdv_mails();
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
        public void SaveMail(pdv_mails item)
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
            var mail = new pdv_mails();
            try
            {
                mail = new pdv_mails
                {
                    id = 0,
                    createDate = DateTime.Now,
                    contractorFromID = contractorFromID,
                    contractorToID = contractorToID
                };
                SaveMail(mail);
            }
            catch (Exception ex)
            {
                _debug(ex, new object { }, "");
            }
            return res;
        }
        public bool DeleteMail(int id, out string msg, pdv_contractors user)
        {
            bool res = false;
            var mail = new pdv_mails();
            try
            {
                mail = GetMail(id);
                if (mail != null)
                {
                    if (mail.contractorFromID != user.id)
                    {
                        msg = "Нет прав на удаление письма";
                        return res;
                    }
                    mail.isDeleted = true;
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
        #region Delivery Mail Statuses
        public List<pdv_deliveryMailStatuses> GetDeliveryMailStatuses()
        {
            var res = new List<pdv_deliveryMailStatuses>();
            try
            {
                res = _db.GetDeliveryMailStatuse().ToList();
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
        }
        public pdv_deliveryMailStatuses GetDeliveryMailStatus(int id)
        {
            var res = new pdv_deliveryMailStatuses();
            try
            {
                res = _db.GetDeliveryMailStatuse().FirstOrDefault(x => x.id == id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
        }
        public int SaveDeliveryMailStatus(pdv_deliveryMailStatuses item, bool withSave = true)
        {
            var res = 0;
            try
            {
                res = _db.SaveDeliveryMailStatuse(item);
            }
            catch (Exception ex)
            {
                _debug(ex, new { item }, "item");
            }
            return res;
        }
        public bool DeleteDeliveryMailStatus(int id)
        {
            var res = false;
            try
            {
               res = _db.DeleteDeliveryMailStatuse(id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { id }, "id");
            }
            return res;
        }
        public bool EditDeliveryMailStatusField(int id, string code, string value, out string msg)
        {
            bool res = false;
            var deliveryMailStatus = new pdv_deliveryMailStatuses();
            try
            {
                deliveryMailStatus = GetDeliveryMailStatus(id);

                if (deliveryMailStatus != null)
                {
                    switch (code)
                    {
                        case "name": deliveryMailStatus.name = value;
                            break;
                        case "code": deliveryMailStatus.code = value;
                            break;

                    }
                    SaveDeliveryMailStatus(deliveryMailStatus);
                    res = true;
                    msg = "Успешно";
                }
                else
                    msg = "Не удалось найти статус документ";
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
                msg = "Произошла ошибка, поле не изменено";
            }
            return res;
        }
        #endregion
        #region Delivery Service
        public List<pdv_deliveryService> GetDeliveryServices()
        {
            var res = new List<pdv_deliveryService>();
            try
            {
                res = _db.GetDeliveryService().ToList();
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
        }
        public pdv_deliveryService GetDeliveryService(int id)
        {
            var res = new pdv_deliveryService();
            try
            {
                res = _db.GetDeliveryService().FirstOrDefault(x => x.id == id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
        }
        public int SaveDeliveryService(pdv_deliveryService item, bool withSave = true)
        {
            var res = 0;
            try
            {
                res = _db.SaveDeliveryService(item);
            }
            catch (Exception ex)
            {
                _debug(ex, new { item }, "item");
            }
            return res;
        }
        public bool DeleteDeliveryService(int id)
        {
            var res = false;
            try
            {
                //RDL.CacheManager.PurgeCacheItems("gl_docStatus");
                res = _db.DeleteDeliveryService(id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { id }, "id");
            }
            return res;
        }
        public bool EditDeliveryServiceField(int id, string code, string value, out string msg)
        {
            bool res = false;
            var deliveryService = new pdv_deliveryService();
            try
            {
                deliveryService = GetDeliveryService(id);

                if (deliveryService != null)
                {
                    switch (code)
                    {
                        case "name": deliveryService.name = value;
                            break;
                        case "code": deliveryService.code = value;
                            break;

                    }
                    SaveDeliveryService(deliveryService);
                    res = true;
                    msg = "Успешно";
                }
                else
                    msg = "Не удалось найти статус документ";
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
                msg = "Произошла ошибка, поле не изменено";
            }
            return res;
        }
        #endregion

    }
}