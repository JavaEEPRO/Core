using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using arkAS.BLL;
using arkAS.BLL.Core;
using arkAS.Areas.Rosh.BLL;
using arkAS.Areas.Rosh.Models;
using arkAS.Models;
using arkAS.BLL.HR;
using arkAS.BLL.Finance;
using System.Data;
using Newtonsoft.Json;
using Dapper;
using System.Collections;
using System.Web.Security;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Web.Http.ModelBinding;
using DocumentFormat.OpenXml.Spreadsheet;

namespace arkAS.Areas.Rosh.Controllers
{
    //[Authorize(Roles = "admin, manager, user")]
    public class OrdersController : BaseController
    {
        public OrdersController()
            : base(new Manager(new Repository(new LocalSqlServer())))
        {

        }

        #region Orders
        //[Authorize(Roles = "admin, manager, user")]
        public ActionResult Orders()
        {
            OrdersViewModel model = new OrdersViewModel();
            model.Orders = mng.Orders.GetOrders();

            model.Contragents = mng.Contragents.GetContragents();
            model.OrdersStatuses = mng.Orders.GetOrderStatuses();
            return View(model);
        }

       /* public ActionResult Test()
        {        
            var parameters = AjaxModel.GetParameters(HttpContext);
            var user = mng.GetUser();
            string msg;
            int total = 0;
            var items = mng.Orders.GetOrdersFiltration(parameters, user, out msg, out total);

            //return Json(new { items }, JsonRequestBehavior.AllowGet);
            var json = JsonConvert.SerializeObject(new
            {
                items,
                msg = msg,
                total = total
            });
            return Content(json, "application/json");
        }*/


        public ActionResult Orders_createItem()
        {
            OrdersViewModel model = new OrdersViewModel();
            model.Contragents = mng.Contragents.GetContragents();
            model.OrdersStatuses = mng.Orders.GetOrderStatuses();
            return PartialView(model);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Orders_createItem(OrdersViewModel model)
        {
            model.Contragents = mng.Contragents.GetContragents();
            model.OrdersStatuses = mng.Orders.GetOrderStatuses();

            string msg = "";
            var user = mng.GetUser();

            mng.Orders.SaveOreder(model, user, out msg);

            ViewBag.Message = msg;  
            //ViewData["Message"] = msg;

            //return PartialView(model);
            return RedirectToAction("Orders");

            //return RedirectToAction("Orders");
        }

        public ActionResult Orders_editItem(int id)
        {
            OrdersViewModel model = new OrdersViewModel();
            model.Contragents = mng.Contragents.GetContragents();
            model.OrdersStatuses = mng.Orders.GetOrderStatuses();

            var item = mng.Orders.GetOrder(id);

            model.id = item.id;
            model.orderDate = item.orderDate;
            model.orderNumber = item.orderNumber;
            model.contragentID = item.contragentID;
            var contragent = mng.Contragents.GetContragent(item.contragentID);
            model.contragentName = contragent.name;

            model.orderStatusID = item.orderStatusID;
            model.description = item.description;
              
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Orders_editItem(OrdersViewModel model)
        {
            model.Contragents = mng.Contragents.GetContragents();
            model.OrdersStatuses = mng.Orders.GetOrderStatuses();
            string msg = "";
            var user = mng.GetUser();

            mng.Orders.SaveOreder(model, user, out msg);

            ViewBag.Message = msg;

            //return RedirectToAction("Orders");
            return PartialView(model);
        }

        public ActionResult Orders_deleteItem(int id)
        {
            OrdersViewModel model = new OrdersViewModel();

            var item = mng.Orders.GetOrder(id);

            model.id = item.id;
            model.orderDate = item.orderDate;
            model.orderNumber = item.orderNumber;
            model.contragentID = item.contragentID;

            var contragent = mng.Contragents.GetContragent(item.contragentID);
            model.contragentName = contragent.name;

            model.orderStatusID = item.orderStatusID;

            var orderStatus = mng.Orders.GetOrderStatus(item.orderStatusID);
            model.orderStatusName = orderStatus.name;

            model.description = item.description;
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Orders_deleteItem")]
        public ActionResult Orders_delete(int id)
        {
            string msg = "";
            var user = mng.GetUser();

            mng.Orders.DeleteOrder(id, user, out msg);
            return RedirectToAction("Orders");
        }

        public ActionResult Orders_searchItem(string orderNumber, int? contragentID, int? orderStatusID)
        {
            string msg ="";

            OrdersViewModel model = new OrdersViewModel();
            model.Contragents = mng.Contragents.GetContragents();
            model.OrdersStatuses = mng.Orders.GetOrderStatuses();

            model.Orders = mng.Orders.SearchOrder(orderNumber, contragentID, orderStatusID, msg);

            ViewBag.Message = msg;

            return View(model);
        }

        #endregion

        #region OrderStatuses

        public ActionResult OrderStatuses()
        {
            OrdersViewModel model = new OrdersViewModel();
            model.OrdersStatuses = mng.Orders.GetOrderStatuses();
            return View(model);
        }

        public ActionResult OrderStatuses_createItem()
        {
            return PartialView();
        }

        public ActionResult OrderStatuses_editItem()
        {
            return PartialView();
        }

        public ActionResult OrderStatuses_deleteItem()
        {
            return PartialView();
        }


        #endregion

    }
}







/*     public ActionResult Orders()
     {
         OrdersViewModel model = new OrdersViewModel();
        // model.Orders = mng.Orders.GetOrders();
        // model.OrdersStatuses = mng.Orders.GetOrderStatuses();
        // model.Contragents = mng.Contragents.GetContragents();
        // return View(model);
         return View();
     }

     public ActionResult Orders()
{
    OrdersViewModel model = new OrdersViewModel();
    model.Orders = mng.Orders.GetOrders();
    model.Contragents = mng.Contragents.GetContragents();
    model.OrdersStatuses = mng.Orders.GetOrderStatuses();
    return View(model);
}

public ActionResult Orders_getItems()
{
    var parameters = AjaxModel.GetParameters(HttpContext);

    var orderDate = "";
    var orderNumber = "";
    var contragentID = "";
    var orderStatusID = "";
    var description = "";

    DateTime createdMin = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
    DateTime createdMax = (DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue;

    if (parameters.filter != null && parameters.filter.Count > 0)
    {
        orderDate = parameters.filter.ContainsKey("orderDate") ? parameters.filter["orderDate"].ToString() : "";
        orderNumber = parameters.filter.ContainsKey("orderNumber") ? parameters.filter["orderNumber"].ToString() : "";
        contragentID = parameters.filter.ContainsKey("contragentID") ? parameters.filter["contragentID"].ToString() : "0";
        orderStatusID = parameters.filter.ContainsKey("orderStatusID") ? parameters.filter["orderStatusID"].ToString() : "0";
        description = parameters.filter.ContainsKey("description") ? parameters.filter["description"].ToString() : "0";

        if (parameters.filter.ContainsKey("orderDate") && parameters.filter["orderDate"] != null)
        {
            var dates = parameters.filter["orderDate"].ToString().Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
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
    p.Add("orderDate", orderDate);
    p.Add("orderNumber", orderNumber);
    p.Add("contragentID", contragentID);
    p.Add("orderStatusID", orderStatusID);
    p.Add("description", description);

    p.Add("createdMin", createdMin);
    p.Add("createdMax", createdMax);
    p.Add("sort1", sort1);
    p.Add("direction1", direction1);
    p.Add("page", parameters.page);
    p.Add("pageSize", parameters.pageSize);
    p.Add("total", dbType: DbType.Int32, direction: ParameterDirection.Output);
    var items = rep.GetSQLData<dynamic>("Rosh_GetOrders", p, CommandType.StoredProcedure) as List<dynamic> ?? new List<dynamic>();

    var total = p.Get<int>("total");

    var json = JsonConvert.SerializeObject(new
    {
        items,
        total = total
    });
    return Content(json, "application/json");
}

     public ActionResult Order_createItem(DateTime orderDate, string orderNumber, int contragentID, int orderStatusID, string description)
     {
         var parameters = AjaxModel.GetAjaxParameters(HttpContext);
        // int orderStatusID = 1;
         var item = new rosh_orders { 
             id = 0,
             orderDate = DateTime.Now.Date,
             orderNumber = orderNumber,
             contragentID = contragentID,
             orderStatusID = orderStatusID,
             description = description,                 
          }; 
         mng.Orders.SaveOreder(item);
         return Json(new
         {
             result = item.id > 0,
             savedID = item.id,
         }, JsonRequestBehavior.AllowGet);
     }

     public ActionResult Order_getItem(int id)
     {
        // var mng = new DocsManager();
         var item = mng.Orders.GetOrder(id);

        // item.path = CopyFileOnFolderID(item.id, item.path);
            
         return Json(new
         {
             id = item.id,
             orderDate = item.orderDate.ToString("dd.MM.yyyy"),
             orderNumber = item.orderNumber,
             contragentID = item.contragentID,
             orderStatusID = item.orderStatusID,
             description = item.description,    
         });
     }

     public ActionResult DocEdit(doc_docs item)
     {
         var mng = new DocsManager();
         mng.SaveDoc(item);
           
         return Json(new { result = true });
     }

     public ActionResult Order_deleteItem(int id)
     {
         var res = false;
      //   var mng = new DocsManager();

         var item = mng.Orders.GetOrder(id);
         if (item != null)
         {
             mng.Orders.DeleteOrder(id);
             res = true;
         }

         return Json(new
         {
             result = res,
             msg = "Документ удален!"
         });
     }

     #endregion


     #region OrderStatuses

     public ActionResult OrderStatuses()
     {
         return View();
     }

     public ActionResult GetOrderStatuses()
     {
         var parameters = AjaxModel.GetParameters(HttpContext);
         var sorts = parameters.sort.Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries);
         var directions = parameters.direction.Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries);
         var sort1 = sorts.Length > 0 ? sorts[0] : "";
         var direction1 = directions.Length > 0 ? directions[0] : "";

         var rep = new CoreRepository();
         var p = new DynamicParameters();
         p.Add("sort1", sort1);
         p.Add("direction1", direction1);
         p.Add("page", parameters.page);
         p.Add("pageSize", parameters.pageSize);
         p.Add("total", dbType: DbType.Int32, direction: ParameterDirection.Output);
         var items = rep.GetSQLData<dynamic>("GetDocStatuses", p, CommandType.StoredProcedure) as List<dynamic> ?? new List<dynamic>();

         var total = p.Get<int>("total");

         var json = JsonConvert.SerializeObject(new
         {
             items,
             total = total
         });
         return Content(json, "application/json");
     }

     public ActionResult SaveOrderStatus()
     {
         var parameters = AjaxModel.GetAjaxParameters(HttpContext);
      //   var mng = new OrderManager();
         var res = false;
         int savedID = 0;
         try
         {
             var fields = (parameters["fields"] as ArrayList).ToArray().ToList().Select(x => x as Dictionary<string, object>).ToList();

             var id = RDL.Convert.StrToInt(AjaxModel.GetValueFromSaveField("id", fields), 0);
             var name = AjaxModel.GetValueFromSaveField("name", fields);
             var code = AjaxModel.GetValueFromSaveField("code", fields);
             var color = AjaxModel.GetValueFromSaveField("color", fields);

             var item = new rosh_orderStatuses { id = id, name = name, code = code, color = color };
             mng.Orders.SaveOrederStatus(item);
             savedID = item.id;
             res = true;
         }
         catch (Exception ex)
         {
             res = false;
         }
         return Json(new
         {
             result = res,
             savedID = savedID,
             msg = ""
         }, JsonRequestBehavior.AllowGet);
     }

     public ActionResult DeleteOrderStatus(int id)
     {
         var res = false;
        // var mng = new OrderManager();

         var item = mng.Orders.GetOrderStatus(id);
         if (item != null)
         {
             mng.Orders.DeleteOrderStatus(id);
             res = true;
         }

         return Json(new
         {
             result = res,
             msg = "Статус удален!"
         });
     }

     #endregion 
}
}*/
