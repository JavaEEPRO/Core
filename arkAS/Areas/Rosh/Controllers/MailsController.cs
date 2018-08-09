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
    public class MailsController : BaseController
    {
        public MailsController()
            : base(new Manager(new Repository(new LocalSqlServer())))
        {

        }

        #region Mails

        public ActionResult Mails()
        {
            MailsViewModel model = new MailsViewModel();
            model.Mails = mng.Mails.GetMails();
            return View(model);
        }

        public ActionResult Mails_createItem()
        {
            return PartialView();
        }

        public ActionResult Mails_editItem()
        {
            return PartialView();
        }

        public ActionResult Mails_deleteItem()
        {
            return PartialView();
        }

        #endregion

        #region MailStatuses

        public ActionResult MailStatuses()
        {
            MailsViewModel model = new MailsViewModel();
            model.mailStatuses = mng.Mails.GetMailStatuses();
            return View(model);
        }

        public ActionResult MailStatuses_createItem()
        {
            return PartialView();
        }

        public ActionResult MailStatuses_editItem()
        {
            return PartialView();
        }

        public ActionResult MailStatuses_deleteItem()
        {
            return PartialView();
        }

        #endregion

        #region SendingSystem

        public ActionResult SendingSystem()
        {
            MailsViewModel model = new MailsViewModel();
            model.SendingSystems = mng.Mails.GetSendingSystems();
            return View(model);
        }

        public ActionResult SendingSystem_createItem()
        {
            return PartialView();
        }

        public ActionResult SendingSystem_editItem()
        {
            return PartialView();
        }

        public ActionResult SendingSystem_deleteItem()
        {
            return PartialView();
        }

        #endregion

    }
}