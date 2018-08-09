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
    public class ContragentsController : BaseController
    {
        public ContragentsController()
            : base(new Manager(new Repository(new LocalSqlServer())))
        {

        }

        #region Contragents

        public ActionResult Contragents()
        {
            ContragentsViewModel model = new ContragentsViewModel();
            model.Contragents = mng.Contragents.GetContragents();
            return View(model);
        }

        public ActionResult Contragents_createItem()
        {
            return PartialView();
        }

        public ActionResult Contragents_editItem()
        {
            return PartialView();
        }

        public ActionResult Contragents_deleteItem()
        {
            return PartialView();
        }

        #endregion

        #region ContragentSourses

        public ActionResult ContragentSourses()
        {
            return View();
        }

        public ActionResult ContragentSourses_createItem()
        {
            return PartialView();
        }

        public ActionResult ContragentSourses_editItem()
        {
            return PartialView();
        }

        public ActionResult ContragentSourses_deleteItem()
        {
            return PartialView();
        }

        #endregion

        #region ContragentStatuses

        public ActionResult ContragentStatuses()
        {
            return View();
        }

        public ActionResult ContragentStatuses_createItem()
        {
            return PartialView();
        }

        public ActionResult ContragentStatuses_editItem()
        {
            return PartialView();
        }

        public ActionResult ContragentStatuses_deleteItem()
        {
            return PartialView();
        }

        #endregion

    }
}