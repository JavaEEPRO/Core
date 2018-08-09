using arkAS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arkAS.Areas.Molchanov.Infrastructure
{
    public class PseudoData
    {
       // public LocalSqlServer db { get; }
        List<molchanov_contragents> _contragentsList;
        List<molchanov_documents> _documentsList;
        List<molchanov_docTypes> _docTypesList;
        List<molchanov_docStatuses> _docStatusesList;
        List<molchanov_invoices> _invoicesList;
        List<molchanov_invoiceStatuses> _invoiceStatusesList;
        List<molchanov_mails> _mailsList;
        List<molchanov_mailStatuses> _mailStausesList;
        List<molchanov_deliverySystems> _deliverySystemsList;
        List<molchanov_logContragents> _logContragnts;
        List<molchanov_logDocuments> _logDocuments;
        List<molchanov_logDocTypes> _logDocTypes;
        List<molchanov_logDocStatuses> _logDocStatuses;
        List<molchanov_logInvoices> _logInvoices;
        List<molchanov_logInvoiceStatuses> _logInvoiceStatuses;
        List<molchanov_logMails> _logMails;
        List<molchanov_logMailStatuses> _logMailsStatuses;
        List<molchanov_logDeliverySystems> _logDeliverySystems;
        public PseudoData()
        {
            #region Contragents PseudoData
            _contragentsList = new List<molchanov_contragents>();
            _contragentsList.Add(new molchanov_contragents { id = 1, name = "Контрагент_1", email = "contragent_1@mail.com" });
            _contragentsList.Add(new molchanov_contragents { id = 2, name = "Контрагент_2", email = "contragent_2@mail.com" });
            _contragentsList.Add(new molchanov_contragents { id = 3, name = "Контрагент_3", email = "contragent_3@mail.com" });
            _contragentsList.Add(new molchanov_contragents { id = 4, name = "Контрагент_4", email = "contragent_4@mail.com" });
            _contragentsList.Add(new molchanov_contragents { id = 5, name = "Контрагент_5", email = "contragent_5@mail.com" });
            #endregion
            #region Documents PseudoData
            _documentsList = new List<molchanov_documents>();
            _documentsList.Add(new molchanov_documents { id = 1, number = "12fsdf", date = DateTime.Today, sum = 100000, description = "Lorem Ipsum", link = "link", uniqueCode = new Guid(), isDeleted = false, contragentID = 1, docParentID = 1, docStatusID = 1, docTypeID = 1 });
            _documentsList.Add(new molchanov_documents { id = 2, number = "1245", date = DateTime.Today, sum = 200000, description = "Lorem Ipsum", link = "link", uniqueCode = new Guid(), isDeleted = false, contragentID = 1, docParentID = 1, docStatusID = 2, docTypeID = 2 });
            _documentsList.Add(new molchanov_documents { id = 3, number = "12876", date = DateTime.Today, sum = 300000, description = "Lorem Ipsum", link = "link", uniqueCode = new Guid(), isDeleted = false, contragentID = 1, docParentID = 1, docStatusID = 3, docTypeID = 3 });
            _documentsList.Add(new molchanov_documents { id = 4, number = "12fsf", date = DateTime.Today, sum = 400000, description = "Lorem Ipsum", link = "link", uniqueCode = new Guid(), isDeleted = false, contragentID = 1, docParentID = 1, docStatusID = 1, docTypeID = 1 });
            _documentsList.Add(new molchanov_documents { id = 5, number = "er3324gfd", date = DateTime.Today, sum = 500000, description = "Lorem Ipsum", link = "link", uniqueCode = new Guid(), isDeleted = false, contragentID = 1, docParentID = 1, docStatusID = 2, docTypeID = 2 });
            _documentsList.Add(new molchanov_documents { id = 6, number = "er3456df", date = DateTime.Today, sum = 600000, description = "Lorem Ipsum", link = "link", uniqueCode = new Guid(), isDeleted = false, contragentID = 1, docParentID = 1, docStatusID = 3, docTypeID = 3 });
            #endregion
            #region Documents Types Pseudo Data
            _docTypesList = new List<molchanov_docTypes>();
            _docTypesList.Add(new molchanov_docTypes{ id = 1, name = "Тип 1", code = "type_1" });
            _docTypesList.Add(new molchanov_docTypes{ id = 2, name = "Тип 2", code = "type_2" });
            _docTypesList.Add(new molchanov_docTypes{ id = 3, name = "Тип 3", code = "type_3" });
            #endregion
            #region Documents Statuses Pseudo Data
            _docStatusesList = new List<molchanov_docStatuses>();
            _docStatusesList.Add(new molchanov_docStatuses { id = 1, name = "Статус 1", code = "status_1" });
            _docStatusesList.Add(new molchanov_docStatuses { id = 2, name = "Статус 2", code = "status_2" });
            _docStatusesList.Add(new molchanov_docStatuses { id = 3, name = "Статус 3", code = "status_3" });
            #endregion
            #region Invoices Pseudo Data
            _invoicesList = new List<molchanov_invoices>();
            _invoicesList.Add(new molchanov_invoices { id = 1, number = "wer134", date = DateTime.Now, description = "LoremIpsum", isDeleted = false, uniqueCode = new Guid(), invStatusID = 1, contragentID = 1 });
            _invoicesList.Add(new molchanov_invoices { id = 2, number = "wer13w454", date = DateTime.Now, description = "LoremIpsum", isDeleted = false, uniqueCode = new Guid(), invStatusID = 2, contragentID = 2 });
            _invoicesList.Add(new molchanov_invoices { id = 3, number = "wer234134", date = DateTime.Now, description = "LoremIpsum", isDeleted = false, uniqueCode = new Guid(), invStatusID = 3, contragentID = 3 });
            _invoicesList.Add(new molchanov_invoices { id = 4, number = "wer2s13434", date = DateTime.Now, description = "LoremIpsum", isDeleted = false, uniqueCode = new Guid(), invStatusID = 1, contragentID = 4 });
            #endregion
            #region InvoiceStatuses Pseudo Data
            _invoiceStatusesList = new List<molchanov_invoiceStatuses>();
            _invoiceStatusesList.Add(new molchanov_invoiceStatuses { id = 1, name = "Статус 1", code = "status_1" });
            _invoiceStatusesList.Add(new molchanov_invoiceStatuses { id = 2, name = "Статус 2", code = "status_2" });
            _invoiceStatusesList.Add(new molchanov_invoiceStatuses { id = 3, name = "Статус 3", code = "status_3" });
            #endregion
            #region Mails Pseudo Data
            _mailsList = new List<molchanov_mails>();
            _mailsList.Add(new molchanov_mails { id = 1, date = DateTime.Now, description = "Lorem ipsum", backDateReceipt = null, trackNumber = "Nr232834-983", backTrackNumber = null, deliverySystemID = 1, fromSender = "sender1", mailStatusID = 1, toRecipient = "recipient1", uniqueCode = new Guid() });
            #endregion
            #region MailsStatuses Pseudo Data
            _mailStausesList = new List<molchanov_mailStatuses>();
            _mailStausesList.Add(new molchanov_mailStatuses { id = 1, name = "Статус 1", code = "status_1" });
            _mailStausesList.Add(new molchanov_mailStatuses { id = 2, name = "Статус 2", code = "status_2" });
            _mailStausesList.Add(new molchanov_mailStatuses { id = 3, name = "Статус 3", code = "status_3" });
            #endregion
            #region DeliverySystems Pseudo Data
            _deliverySystemsList = new List<molchanov_deliverySystems>();
            _deliverySystemsList.Add(new molchanov_deliverySystems { id = 1, name = "Система 1", code = "system_1" });
            #endregion
            #region Logs
            _logContragnts = new List<molchanov_logContragents>();
            _logDocuments = new List<molchanov_logDocuments>();
            _logDocTypes = new List<molchanov_logDocTypes>();
            _logDocStatuses = new List<molchanov_logDocStatuses>();
            _logInvoices = new List<molchanov_logInvoices>();
            _logInvoiceStatuses = new List<molchanov_logInvoiceStatuses>();
            _logMails = new List<molchanov_logMails>();
            _logMailsStatuses = new List<molchanov_logMailStatuses>();
            _logDeliverySystems = new List<molchanov_logDeliverySystems>();
            #endregion
        }
        #region Contragents methods and logs
        public List<molchanov_contragents> GetContragents()
        {
            return _contragentsList;
        }
        public molchanov_contragents GetContragent(int id)
        {
            var res = _contragentsList.FirstOrDefault(x => x.id == id);
            return res;
        }
        public int AddContragent(molchanov_contragents element)
        {
            _contragentsList.Add(element);
            return element.id;
        }
        public molchanov_contragents ChangeContragent(int id)
        {
            var res = _contragentsList.FirstOrDefault(c => c.id == id);
            return res;
        }
        public bool DeleteContragent(int id)
        {
            var item = _contragentsList.FirstOrDefault(x => x.id == id);
            _contragentsList.Remove(item);
            return true;
        }
        public List<molchanov_logContragents> GetLogContragent()
        {
            return _logContragnts;
        }
        public void AddLogContragent(molchanov_logContragents element)
        {
            _logContragnts.Add(element);
        }
        #endregion
        #region Documents methods and logs
        public List<molchanov_documents> GetDocuments()
        {
            return _documentsList;
        }
        public molchanov_documents GetDocument(int id)
        {
            var res = _documentsList.FirstOrDefault(x => x.id == id);
            return res;
        }
        public int AddDocument(molchanov_documents element)
        {
            _documentsList.Add(element);
            return element.id;
        }
        public molchanov_documents ChangeDocument(int id)
        {
            var res = _documentsList.FirstOrDefault(c => c.id == id);
            return res;
        }
        public bool DeleteDocument(int id)
        {
            var item = _documentsList.FirstOrDefault(x => x.id == id);
            _documentsList.Remove(item);
            return true;
        }
        public List<molchanov_logDocuments> GetLogDocument()
        {
            return _logDocuments;
        }
        public void AddLogDocument(molchanov_logDocuments element)
        {
            _logDocuments.Add(element);
        }
        #endregion
        #region DocTypes methods and logs
        public List<molchanov_docTypes> GetDocTypes()
        {
            return _docTypesList;
        }
        public molchanov_docTypes GetDocType(int id)
        {
            var res = _docTypesList.FirstOrDefault(x => x.id == id);
            return res;
        }
        public int AddDocTypes(molchanov_docTypes element)
        {
            _docTypesList.Add(element);
            return element.id;
        }
        public molchanov_docTypes ChangeDocTypes(int id)
        {
            var res = _docTypesList.FirstOrDefault(c => c.id == id);
            return res;
        }
        public bool DeleteDocTypes(int id)
        {
            var item = _docTypesList.FirstOrDefault(x => x.id == id);
            _docTypesList.Remove(item);
            return true;
        }
        public List<molchanov_logDocTypes> GetLogDocTypes()
        {
            return _logDocTypes;
        }
        public void AddLogDocTypes(molchanov_logDocTypes element)
        {
            _logDocTypes.Add(element);
        }
        #endregion
        #region DocStatuses methods and logs
        public List<molchanov_docStatuses> GetDocumentStatuses()
        {
            return _docStatusesList;
        }
        public molchanov_docStatuses GetDocumentStatus(int id)
        {
            var res = _docStatusesList.FirstOrDefault(x => x.id == id);
            return res;
        }
        public int AddDocStatus(molchanov_docStatuses element)
        {
            _docStatusesList.Add(element);
            return element.id;
        }
        public molchanov_docStatuses ChangeDocStatus(int id)
        {
            var res = _docStatusesList.FirstOrDefault(c => c.id == id);
            return res;
        }
        public bool DeleteDocStatus(int id)
        {
            var item = _docStatusesList.FirstOrDefault(x => x.id == id);
            _docStatusesList.Remove(item);
            return true;
        }
        public List<molchanov_logDocStatuses> GetLogDocStatuses()
        {
            return _logDocStatuses;
        }
        public void AddLogDocStatuses(molchanov_logDocStatuses element)
        {
            _logDocStatuses.Add(element);
        }
        #endregion
        #region Invoices methods and logs
        public List<molchanov_invoices> GetInvoices()
        {
            return _invoicesList;
        }
        public molchanov_invoices GetInvoice(int id)
        {
            var res = _invoicesList.FirstOrDefault(x => x.id == id);
            return res;
        }
        public int AddInvoice(molchanov_invoices element)
        {
            _invoicesList.Add(element);
            return element.id;
        }
        public molchanov_invoices ChangeInvoice(int id)
        {
            var res = _invoicesList.FirstOrDefault(c => c.id == id);
            return res;
        }
        public bool DeleteInvoice(int id)
        {
            var item = _invoicesList.FirstOrDefault(x => x.id == id);
            _invoicesList.Remove(item);
            return true;
        }
        public List<molchanov_logInvoices> GetLogInvoices()
        {
            return _logInvoices;
        }
        public void AddLogInvoices(molchanov_logInvoices element)
        {
            _logInvoices.Add(element);
        }
        #endregion
        #region Invoice Statuses methods and logs
        public List<molchanov_invoiceStatuses> GetInvoiceStatuses()
        {
            return _invoiceStatusesList;
        }
        public molchanov_invoiceStatuses GetInvoiceStatus(int id)
        {
            var res = _invoiceStatusesList.FirstOrDefault(x => x.id == id);
            return res;
        }
        public int AddInvoiceStatus(molchanov_invoiceStatuses element)
        {
            _invoiceStatusesList.Add(element);
            return element.id;
        }
        public molchanov_invoiceStatuses ChangeInvoiceStatus(int id)
        {
            var res = _invoiceStatusesList.FirstOrDefault(c => c.id == id);
            return res;
        }
        public bool DeleteInvoiceStatus(int id)
        {
            var item = _invoiceStatusesList.FirstOrDefault(x => x.id == id);
            _invoiceStatusesList.Remove(item);
            return true;
        }
        public List<molchanov_logInvoiceStatuses> GetLogInvoice()
        {
            return _logInvoiceStatuses;
        }
        public void AddLogInvoiceStatuses(molchanov_logInvoiceStatuses element)
        {
            _logInvoiceStatuses.Add(element);
        }
        #endregion
        #region Mails methods and logs
        public List<molchanov_mails> GetMails()
        {
            return _mailsList;
        }
        public molchanov_mails GetMail(int id)
        {
            var res = _mailsList.FirstOrDefault(x => x.id == id);
            return res;
        }
        public int AddMail(molchanov_mails element)
        {
            _mailsList.Add(element);
            return element.id;
        }
        public molchanov_mails ChangeMail(int id)
        {
            var res = _mailsList.FirstOrDefault(c => c.id == id);
            return res;
        }
        public bool DeleteMail(int id)
        {
            var item = _mailsList.FirstOrDefault(x => x.id == id);
            _mailsList.Remove(item);
            return true;
        }
        public List<molchanov_logMails> GetLogMails()
        {
            return _logMails;
        }
        public void AddLogMails(molchanov_logMails element)
        {
            _logMails.Add(element);
        }
        #endregion
        #region Mail Statuses methods and logs
        public List<molchanov_mailStatuses> GetMailStatuses()
        {
            return _mailStausesList;
        }
        public molchanov_mailStatuses GetMailStatus(int id)
        {
            var res = _mailStausesList.FirstOrDefault(x => x.id == id);
            return res;
        }
        public int AddMailStatus(molchanov_mailStatuses element)
        {
            _mailStausesList.Add(element);
            return element.id;
        }
        public molchanov_mailStatuses ChangeMailStatus(int id)
        {
            var res = _mailStausesList.FirstOrDefault(c => c.id == id);
            return res;
        }
        public bool DeleteMailStatus(int id)
        {
            var item = _mailStausesList.FirstOrDefault(x => x.id == id);
            _mailStausesList.Remove(item);
            return true;
        }
        public List<molchanov_logMailStatuses> GetLogMailStatuses()
        {
            return _logMailsStatuses;
        }
        public void AddLogMailStatuses(molchanov_logMailStatuses element)
        {
            _logMailsStatuses.Add(element);
        }
        #endregion
        #region DeliverySystems methods and logs
        public List<molchanov_deliverySystems> GetDDeliverySystems()
        {
            return _deliverySystemsList;
        }
        public molchanov_deliverySystems GetDeliverySystem(int id)
        {
            var res = _deliverySystemsList.FirstOrDefault(x => x.id == id);
            return res;
        }
        public int AddDeliveryStatus(molchanov_deliverySystems element)
        {
            _deliverySystemsList.Add(element);
            return element.id;
        }
        public molchanov_deliverySystems ChangeDeliverySystems(int id)
        {
            var res = _deliverySystemsList.FirstOrDefault(c => c.id == id);
            return res;
        }
        public bool DeleteDeliverySystems(int id)
        {
            var item = _deliverySystemsList.FirstOrDefault(x => x.id == id);
            _deliverySystemsList.Remove(item);
            return true;
        }
        public List<molchanov_logDeliverySystems> GetLogDeliverySystems()
        {
            return _logDeliverySystems;
        }
        public void AddLogDeliverySystems(molchanov_logDeliverySystems element)
        {
            _logDeliverySystems.Add(element);
        }
        #endregion
    }
}