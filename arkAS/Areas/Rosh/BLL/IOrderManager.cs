using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using arkAS.BLL;
using arkAS.Models;
using arkAS.Areas.Rosh.Models;

namespace arkAS.Areas.Rosh.BLL
{
    public interface IOrderManager : IDisposable
    {
        #region System
       // bool _CanChangeOrder(aspnet_Users user);
        bool CanCurrentUserChangeOrder();
        #endregion

        //#region orders
        //List<rosh_orders> GetOrders();
        //dynamic GetOrdersFiltration(ASCRUDGetItemsModel parameters, aspnet_Users user, out string msg, out int total)
        //rosh_orders GetOrder(int id);
        //void SaveOreder(rosh_orders oreder);
        //void DeleteOrder(int id);
        //#endregion

        #region orders
        List<rosh_orders> GetOrders();
        rosh_orders GetOrder(int id);
        void SaveOreder(OrdersViewModel model, aspnet_Users user, out string msg);
        void ChangeOrderStatus(int savedOrderID);
        void DeleteOrder(int id, aspnet_Users user, out string msg);
        List<rosh_orders> SearchOrder(string orderNumber, int? contragentID, int? orderStatusID, string msg);
        #endregion

        #region orderStatuses
        List<rosh_orderStatuses> GetOrderStatuses();
        rosh_orderStatuses GetOrderStatus(int id);
        void SaveOrederStatus(rosh_orderStatuses orederStatus);
        void DeleteOrderStatus(int id);
        #endregion

        #region orderLogs
        List<rosh_orderLogs> GetOrderLogs();
        rosh_orderLogs GetOrderLog(int id);
        void SaveOrederLog(rosh_orderLogs orederLog);
        void DeleteOrderLog(int id);
        #endregion

    }
}
