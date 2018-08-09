using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arkAS.BLL;
using arkAS.BLL.Core;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using arkAS.Models;
using Dapper;
using System.Data;
using arkAS.Areas.Rosh.Models;

namespace arkAS.Areas.Rosh.BLL
{
    public class OrderManager: IOrderManager
    {
        #region System
        public IReposirory db;
        public IManager mng;
        private bool _disposed;

        public OrderManager(IReposirory db, IManager mng)
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
            if (!_disposed)
            {
                if (disposing)
                {
                    if (db != null)
                        db.Dispose();
                }
                db = null;
                _disposed = true;
            }
        }

        private void _debug(Exception ex, Object parameters, string additions = "")
        {
            RDL.Debug.LogError(ex, additions, parameters);
        }

        private bool _CanChangeOrder(aspnet_Users user)
        {
            var res = false;

            if (user.UserName == "rosh@email.com" || user.UserName == "roshAdmin@email.com")
            {
                res = true;
            }
            return res;
        }

        public bool CanCurrentUserChangeOrder()
        {
            return _CanChangeOrder(mng.GetUser());
        }


        #endregion

        #region orders
        public List<rosh_orders> GetOrders()
        {

            var res = new List<rosh_orders>();
            try
            {
                res = db.GetOrders().ToList();
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
        }

        public List<rosh_orders> SearchOrder(string orderNumber, int? contragentID, int? orderStatusID, string msg)
        {
            msg = "";

            var orders = from o in db.GetOrders()
                         select o;
            try
            {

                if (!String.IsNullOrEmpty(orderNumber))
                {
                    orders = orders.Where(o => o.orderNumber.Contains(orderNumber));
                }
                else
                    msg = "Счет с таким номером не существует!";

                if (contragentID != 0)
                {
                    orders = orders.Where(o => o.contragentID == contragentID);
                }

                if(orderStatusID != 0)
                {
                    orders = orders.Where(o => o.orderStatusID == orderStatusID);
                }

            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
            return orders.ToList();
        }

        public rosh_orders GetOrder(int id)
        {
            var res = new rosh_orders();
            try
            {
                res = db.GetOrder(id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { id }, "");
            }
            return res;
        }

        public void SaveOreder(OrdersViewModel model, aspnet_Users user, out string msg)
        {
            msg = "";
            try
            {
                if (!_CanChangeOrder(user))
                {
                    msg = "Нет прав для данной операции";
                }
                else
                {
                    if (model.id == 0) //when order not exist...
                    {
                        var item = new rosh_orders
                        {
                            id = 0,
                            orderDate = DateTime.Now.Date,
                            orderNumber = model.orderNumber,
                            contragentID = model.contragentID,
                            orderStatusID = model.orderStatusID,
                            description = model.description
                        };

                        db.SaveOreder(item);
                        msg = "Счет успешно создан!";

                        int savedOrderID = item.id; // getting id of created item
                        mng.Orders.ChangeOrderStatus(savedOrderID);
                    }
                    else
                    {
                        var item = new rosh_orders
                        {
                            id = model.id,
                            orderDate = model.orderDate,
                            orderNumber = model.orderNumber,
                            contragentID = model.contragentID,
                            orderStatusID = model.orderStatusID,
                            description = model.description
                        };

                        db.SaveOreder(item);
                        msg = "Счет успешно изменен!";

                        int savedOrderID = item.id; // getting id of changed item
                        mng.Orders.ChangeOrderStatus(savedOrderID);
                    }
                }
            }
            catch (Exception ex)
            {
                _debug(ex, new { model }, "");
                msg = "Сбой операции";
            }
        } 

        //public void SaveOreder(rosh_orders res, aspnet_Users user, out string msg)
        //{
        //    msg = "";

        //    try
        //    {
        //        if (!_CanChangeOrder(user))
        //        {
        //            msg = "Нет прав для данной операции";
        //        }

        //        else
        //        {
        //            db.SaveOreder(res);
        //            msg = "Счет успешно сохранен";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _debug(ex, new { res }, "");
        //        msg = "Сбой операции";
        //    }
        //} 

        public void ChangeOrderStatus(int savedOrderID)
        {
            if (savedOrderID != 0)
            {
                var savedOrder = db.GetOrder(savedOrderID); //last saved or changed order
                var orderStatus = db.GetOrderStatus(savedOrder.orderStatusID); // orderStatus of last changed order

                var orderLog = new rosh_orderLogs
                {
                    date = DateTime.Now.Date,
                    orderID = savedOrder.id,
                    changes = savedOrder.orderNumber + ", " + "статус: " + orderStatus.name + ", " + savedOrder.description,
                };
                mng.Orders.SaveOrederLog(orderLog);
            }
        }

        public void DeleteOrder(int id, aspnet_Users user, out string msg)
        {
            msg = "";

            try
            {
                if (!_CanChangeOrder(user))
                {
                    msg = "Нет прав для данной операции";
                }

                else
                {
                    db.DeleteOrder(id);
                    msg = "Счет успешно удален";
                }
            }
            catch (Exception ex)
            {
                _debug(ex, new { id }, "");
                msg = "Сбой операции";
            }
        }

        #endregion

        #region orderStatuses
        public List<rosh_orderStatuses> GetOrderStatuses()
        {
            var res = new List<rosh_orderStatuses>();
            try
            {
                res = db.GetOrderStatuses().ToList();
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
        }

        public rosh_orderStatuses GetOrderStatus(int id)
        {
            var res = new rosh_orderStatuses();
            try
            {
                res = db.GetOrderStatus(id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { id }, "");
            }
            return res;
        }

        public void SaveOrederStatus(rosh_orderStatuses res)
        {
            try
            {
                db.SaveOrederStatus(res);
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
        }

        public void DeleteOrderStatus(int id)
        {
            try
            {
                db.DeleteOrderStatus(id);
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
        }
        #endregion

        #region orderLogs
        public List<rosh_orderLogs> GetOrderLogs()
        {
            var res = new List<rosh_orderLogs>();
            try
            {
                res = db.GetOrderLogs().ToList();
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
        }

        public rosh_orderLogs GetOrderLog(int id)
        {
            var res = new rosh_orderLogs();
            try
            {
                res = db.GetOrderLog(id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { id }, "");
            }
            return res;
        }

        public void SaveOrederLog(rosh_orderLogs res)
        {
            try
            {
                db.SaveOrederLog(res);
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
        }

        public void DeleteOrderLog(int id)
        {
            try
            {
                db.DeleteOrderLog(id);
            }
            catch (Exception ex)
            {
                RDL.Debug.LogError(ex);
            }
        }
        #endregion

    }
}