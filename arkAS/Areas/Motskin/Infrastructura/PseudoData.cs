using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using arkAS.BLL;

namespace arkAS.Areas.Motskin.Infrastructura
{
    public abstract class PseudoData
    {
        private static List<motskin_documents> _documents;
        private static List<motskin_contractors> _contractors;
        private static List<motskin_invoices> _invoices;
        private static List<motskin_mails> _mails;
        private static List<motskin_documentStatusLog> _documentStatusLog;
        private static List<motskin_invoiceStatusLog> _invoiceStatusLog;
        private static List<motskin_mailStatusLog> _mailStatusLog;
        private static List<motskin_sendSystems> _sendSystems;
        private static List<motskin_documentTypes> _documentTypes;
        private static List<motskin_documentStatuses> _documentStatuses;
        private static List<motskin_invoiceStatuses> _invoiceStatuses;
        private static List<motskin_mailStatuses> _mailStatuses;

        static PseudoData()
        {
            #region Contractors
            _contractors = new List<motskin_contractors>();
            _contractors.Add(new motskin_contractors { id = _contractors.Count + 1, name = "Гомельский государственный технический университет имени П.О. Сухого" });
            _contractors.Add(new motskin_contractors { id = _contractors.Count + 1, name = "ИП Бобров А.Н." });
            _contractors.Add(new motskin_contractors { id = _contractors.Count + 1, name = "Microchip Technology Inc." });
            _contractors.Add(new motskin_contractors { id = _contractors.Count + 1, name = "АО «Морской порт Санкт-Петербург»" });
            _contractors.Add(new motskin_contractors { id = _contractors.Count + 1, name = "Grand Hyatt Shenzhen" });
            _contractors.Add(new motskin_contractors { id = _contractors.Count + 1, name = "ПАО КБ «Уральский банк реконструкции и развития»" });
            _contractors.Add(new motskin_contractors { id = _contractors.Count + 1, name = "Белорусская железная дорога" });

            #endregion

            #region SendSystem
            _sendSystems = new List<motskin_sendSystems>();
            _sendSystems.Add(new motskin_sendSystems { id = _sendSystems.Count + 1, name = "e-mail" });
            _sendSystems.Add(new motskin_sendSystems { id = _sendSystems.Count + 1, name = "Почта России" });
            _sendSystems.Add(new motskin_sendSystems { id = _sendSystems.Count + 1, name = "Белпочта" });
            _sendSystems.Add(new motskin_sendSystems { id = _sendSystems.Count + 1, name = "ТК \"Кит\"" });
            _sendSystems.Add(new motskin_sendSystems { id = _sendSystems.Count + 1, name = "ТК \"Энергия\"" });
            #endregion

            #region DocumentType
            _documentTypes = new List<motskin_documentTypes>();
            _documentTypes.Add(new motskin_documentTypes { id = _documentTypes.Count + 1, name = "Акт", code = "act" });
            _documentTypes.Add(new motskin_documentTypes { id = _documentTypes.Count + 1, name = "Договор", code = "contract" });
            _documentTypes.Add(new motskin_documentTypes { id = _documentTypes.Count + 1, name = "Доп. соглашение", code = "additionalAgreement" });
            #endregion

            #region DocumentStatuses
            _documentStatuses = new List<motskin_documentStatuses>();
            _documentStatuses.Add(new motskin_documentStatuses { id = _documentStatuses.Count + 1, name = "создан", code = "created" });
            _documentStatuses.Add(new motskin_documentStatuses { id = _documentStatuses.Count + 1, name = "на согласовании", code = "forAgreement" });
            _documentStatuses.Add(new motskin_documentStatuses { id = _documentStatuses.Count + 1, name = "подписан", code = "signed" });
            _documentStatuses.Add(new motskin_documentStatuses { id = _documentStatuses.Count + 1, name = "получена утверждённая копия", code = "approvedCopy" });

            #endregion

            #region InvoiceStatuses
            _invoiceStatuses = new List<motskin_invoiceStatuses>();
            _invoiceStatuses.Add(new motskin_invoiceStatuses { id = _invoiceStatuses.Count + 1, name = "создан", code = "created" });
            _invoiceStatuses.Add(new motskin_invoiceStatuses { id = _invoiceStatuses.Count + 1, name = "выставлен", code = "madeOut" });

            #endregion

            #region MailStatuses
            _mailStatuses = new List<motskin_mailStatuses>();
            _mailStatuses.Add(new motskin_mailStatuses { id = _mailStatuses.Count + 1, name = "создан", code = "created" });
            _mailStatuses.Add(new motskin_mailStatuses { id = _mailStatuses.Count + 1, name = "согласована отправка", code = "approvedSending" });
            _mailStatuses.Add(new motskin_mailStatuses { id = _mailStatuses.Count + 1, name = "отправлено", code = "sent" });
            _mailStatuses.Add(new motskin_mailStatuses { id = _mailStatuses.Count + 1, name = "получено", code = "received" });
            _mailStatuses.Add(new motskin_mailStatuses { id = _mailStatuses.Count + 1, name = "обратно отправлено", code = "backSent" });
            _mailStatuses.Add(new motskin_mailStatuses { id = _mailStatuses.Count + 1, name = "обратно получено", code = "backReceived" });
            _mailStatuses.Add(new motskin_mailStatuses { id = _mailStatuses.Count + 1, name = "закрыто", code = "closed" });

            #endregion

            #region Documents
            _documents = new List<motskin_documents>();
            _documents.Add(new motskin_documents
            {
                id = _documents.Count + 1,
                number = "123-43BY17",
                sum = 45231,
                date = DateTime.ParseExact("2017-01-18 10:32:32", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                comment = "Автоматизация рабочего места бригадира",
                reference = "Sources/123-43BY17.docx",
                isDeleted = false,
                motskin_contractors = GetContractors().First(p => p.name == "АО «Морской порт Санкт-Петербург»"),
                motskin_documentStatuses = GetDocumentStatuses().First(p => p.code == "approvedCopy"),
                motskin_documentTypes = GetDocumentTypes().First(p => p.code == "contract"),
            });
            _documents.Add(new motskin_documents
            {
                id = _documents.Count + 1,
                number = "333-22BY16",
                sum = 111000,
                date = DateTime.ParseExact("2016-04-18 10:32:32", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                comment = "Сопровождение проекта удалённого диагнастирования",
                reference = "Sources/333-22BY16.docx",
                isDeleted = false,
                motskin_contractors = GetContractors().First(p => p.name == "Microchip Technology Inc."),
                motskin_documentStatuses = GetDocumentStatuses().First(p => p.code == "created"),
                motskin_documentTypes = GetDocumentTypes().First(p => p.code == "act")
            });
            foreach (motskin_documents el in _documents)
            {
                el.contractorID = el.motskin_contractors.id;
                el.documentStatusID = el.motskin_documentStatuses.id;
                el.documentTypeID = el.motskin_documentTypes.id;
            }
            #endregion

            #region Invoices
            _invoices = new List<motskin_invoices>();
            _invoices.Add(new motskin_invoices
            {
                id = _invoices.Count + 1,
                number = "i653BY17",
                date = DateTime.ParseExact("2017-02-12 16:22:12", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                comment = "10 лекций по ASP.NET",
                isDeleted = false,
                motskin_contractors = GetContractors().First(p => p.name == "Гомельский государственный технический университет имени П.О. Сухого"),
                motskin_invoiceStatuses = GetInvoiceStatuses().First(p => p.code == "madeOut"),
            });
            foreach (motskin_invoices el in _invoices)
            {
                el.contractorID = el.motskin_contractors.id;
                el.invoiceStatusID = el.motskin_invoiceStatuses.id;
            }
            #endregion

            #region Mails
            _mails = new List<motskin_mails>();
            _mails.Add(new motskin_mails
            {
                id = _mails.Count + 1,
                date = DateTime.ParseExact("2017-01-18 10:32:32", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                fromAddress = "motskin@tut.by",
                toAddress = "motskinigor@gmail.com",
                description = "Проверка работы системы",
                trackNumber = "12345678BY",
                backTrackNumber = "87654321BY",
                backDateReceived = DateTime.ParseExact("2017-03-24 11:21:23", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                isDeleted = false,
                motskin_sendSystems = GetSendSystems().First(p => p.name == "e-mail"),
                motskin_mailStatuses = GetMailStatuses().First(p => p.code == "backReceived"),
            });
            _mails.Add(new motskin_mails
            {
                id = _mails.Count + 1,
                date = DateTime.ParseExact("2017-03-17 11:11:11", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                fromAddress = "г. Гомель",
                toAddress = "г. Минск",
                description = "Доставка деталей для принтера",
                trackNumber = null,
                backTrackNumber = null,
                backDateReceived = null,
                isDeleted = false,
                motskin_sendSystems = GetSendSystems().First(p => p.name == "Белпочта"),
                motskin_mailStatuses = GetMailStatuses().First(p => p.code == "approvedSending"),
            });
            foreach (motskin_mails el in _mails)
            {
                el.mailStatusID = el.motskin_mailStatuses.id;
                el.sendSystemID = el.motskin_sendSystems.id;
            }
            #endregion

            #region DocumentStatusLog
            _documentStatusLog = new List<motskin_documentStatusLog>();
            _documentStatusLog.Add(CreateDocumentStatusLog(_documentStatusLog.Count + 1, "2017-01-18 10:32:32", "Мотькин И.С.", "123-43BY17", "created"));
            _documentStatusLog.Add(CreateDocumentStatusLog(_documentStatusLog.Count + 1, "2017-01-22 17:49:04", "Мотькин И.С.", "123-43BY17", "forAgreement"));
            _documentStatusLog.Add(CreateDocumentStatusLog(_documentStatusLog.Count + 1, "2017-02-15 07:12:43", "Петренко Д.Ю.", "123-43BY17", "signed"));
            _documentStatusLog.Add(CreateDocumentStatusLog(_documentStatusLog.Count + 1, "2017-03-03 12:32:11", "Мотькин И.С.", "123-43BY17", "approvedCopy"));
            _documentStatusLog.Add(CreateDocumentStatusLog(_documentStatusLog.Count + 1, "2016-04-18 10:32:32", "Мотькин И.С.", "333-22BY16", "created"));
            #endregion

            #region InvoiceStatusLog
            _invoiceStatusLog = new List<motskin_invoiceStatusLog>();
            _invoiceStatusLog.Add(CreateInvoiceStatusLog(_invoiceStatusLog.Count + 1, "2017-02-12 16:22:12", "Захаров А.В.", "i653BY17", "created"));
            _invoiceStatusLog.Add(CreateInvoiceStatusLog(_invoiceStatusLog.Count + 1, "2017-02-23 15:02:59", "Мотькин И.С.", "i653BY17", "madeOut"));
            #endregion

            #region MailStatusLog
            _mailStatusLog = new List<motskin_mailStatusLog>();
            _mailStatusLog.Add(CreateMailStatusLog(_mailStatusLog.Count + 1, "2017-01-18 10:32:32", "Мотькин И.С.", "Проверка работы системы", "created"));
            _mailStatusLog.Add(CreateMailStatusLog(_mailStatusLog.Count + 1, "2017-01-22 17:49:04", "Мотькин И.С.", "Проверка работы системы", "approvedSending"));
            _mailStatusLog.Add(CreateMailStatusLog(_mailStatusLog.Count + 1, "2017-02-15 07:12:43", "Петренко Д.Ю.", "Проверка работы системы", "sent"));
            _mailStatusLog.Add(CreateMailStatusLog(_mailStatusLog.Count + 1, "2017-02-03 12:32:11", "Никифоров О.В.", "Проверка работы системы", "received"));
            _mailStatusLog.Add(CreateMailStatusLog(_mailStatusLog.Count + 1, "2017-03-03 09:12:11", "Никифоров О.В.", "Проверка работы системы", "backSent"));
            _mailStatusLog.Add(CreateMailStatusLog(_mailStatusLog.Count + 1, "2017-03-24 11:21:23", "Петренко Д.Ю.", "Проверка работы системы", "backReceived"));

            _mailStatusLog.Add(CreateMailStatusLog(_mailStatusLog.Count + 1, "2017-02-28 16:11:54", "Мартынова Е.П.", "Доставка деталей для принтера", "created"));
            _mailStatusLog.Add(CreateMailStatusLog(_mailStatusLog.Count + 1, "2017-03-02 10:07:28", "Мартынова Е.П.", "Доставка деталей для принтера", "approvedSending"));
            #endregion
        }


        #region Creaters Method StatusLog
        private static motskin_documentStatusLog CreateDocumentStatusLog(int id, string created, string note, string number, string statusCode)
        {
            return new motskin_documentStatusLog
            {
                id = id,
                created = DateTime.ParseExact(created, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                note = note,
                motskin_documents = GetDocuments().First(p => p.number == number),
                documentID = GetDocuments().First(p => p.number == number).id,
                motskin_documentStatuses = GetDocumentStatuses().First(p => p.code == statusCode),
                documentStatusID = GetDocumentStatuses().First(p => p.code == statusCode).id,
            };
        }

        private static motskin_invoiceStatusLog CreateInvoiceStatusLog(int id, string created, string note, string number, string statusCode)
        {
            return new motskin_invoiceStatusLog
            {
                id = id,
                created = DateTime.ParseExact(created, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                note = note,
                invoiceID = GetInvoices().First(p => p.number == number).id,
                motskin_invoices = GetInvoices().First(p => p.number == number),
                invoiceStatusID = GetInvoiceStatuses().First(p => p.code == statusCode).id,
                motskin_invoiceStatuses = GetInvoiceStatuses().First(p => p.code == statusCode)
            };
        }

        private static motskin_mailStatusLog CreateMailStatusLog(int id, string created, string note, string description, string statusCode)
        {
            return new motskin_mailStatusLog
            {
                id = id,
                created = DateTime.ParseExact(created, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                note = note,
                mailID = GetMails().First(p => p.description == description).id,
                motskin_mails = GetMails().First(p => p.description == description),
                mailStatusID = GetMailStatuses().First(p => p.code == statusCode).id,
                motskin_mailStatuses = GetMailStatuses().First(p => p.code == statusCode)
            };
        }
        #endregion

        #region SendSystem Get-Set
        public static IQueryable<motskin_sendSystems> GetSendSystems()
        {
            return _sendSystems.AsQueryable();
        }

        public static int SaveSendSystem(motskin_sendSystems element, bool withSave = true)
        {
            if (element.id == 0)
            {
                element.id = _sendSystems.Count + 1;
                _sendSystems.Add(element);
            }
            else
            {
                int pos = _sendSystems.IndexOf(_sendSystems.FirstOrDefault(p => p.id == element.id));
                if (pos >= 0)
                    _sendSystems[pos] = element;
            }
            return _sendSystems.Count;
        }
        public static bool DeleteSendSystem(int id)
        {
            motskin_sendSystems element = _sendSystems.FirstOrDefault(p => p.id == id);
            if (element != null)
            {
                _sendSystems.Remove(element);
                return true;
            }
            return false;
        }
        #endregion

        #region DocumentType Get-Set
        public static IQueryable<motskin_documentTypes> GetDocumentTypes()
        {
            return _documentTypes.AsQueryable();
        }

        public static int SaveDocumentTypes(motskin_documentTypes element, bool withSave = true)
        {
            if (element.id == 0)
            {
                element.id = _documentTypes.Count + 1;
                _documentTypes.Add(element);
            }
            else
            {
                int pos = _documentTypes.IndexOf(_documentTypes.FirstOrDefault(p => p.id == element.id));
                if (pos >= 0)
                    _documentTypes[pos] = element;
            }
            return _documentTypes.Count;
        }
        public static bool DeleteDocumentTypes(int id)
        {
            motskin_documentTypes element = _documentTypes.FirstOrDefault(p => p.id == id);
            if (element != null)
            {
                _documentTypes.Remove(element);
                return true;
            }
            return false;
        }
        #endregion

        #region DocumentStatuses Get-Set
        public static IQueryable<motskin_documentStatuses> GetDocumentStatuses()
        {
            return _documentStatuses.AsQueryable();
        }

        public static int SaveDocumentStatuses(motskin_documentStatuses element, bool withSave = true)
        {
            if (element.id == 0)
            {
                element.id = _documentStatuses.Count + 1;
                _documentStatuses.Add(element);
            }
            else
            {
                int pos = _documentStatuses.IndexOf(_documentStatuses.FirstOrDefault(p => p.id == element.id));
                if (pos >= 0)
                    _documentStatuses[pos] = element;
            }
            return _documentStatuses.Count;
        }
        public static bool DeleteDocumentStatuses(int id)
        {
            motskin_documentStatuses element = _documentStatuses.FirstOrDefault(p => p.id == id);
            if (element != null)
            {
                _documentStatuses.Remove(element);
                return true;
            }
            return false;
        }
        #endregion

        #region InvoiceStatuses Get-Set
        public static IQueryable<motskin_invoiceStatuses> GetInvoiceStatuses()
        {
            return _invoiceStatuses.AsQueryable();
        }

        public static int SaveInvoiceStatuses(motskin_invoiceStatuses element, bool withSave = true)
        {
            if (element.id == 0)
            {
                element.id = _invoiceStatuses.Count + 1;
                _invoiceStatuses.Add(element);
            }
            else
            {
                int pos = _invoiceStatuses.IndexOf(_invoiceStatuses.FirstOrDefault(p => p.id == element.id));
                if (pos >= 0)
                    _invoiceStatuses[pos] = element;
            }
            return _invoiceStatuses.Count;
        }
        public static bool DeleteInvoiceStatuses(int id)
        {
            motskin_invoiceStatuses element = _invoiceStatuses.FirstOrDefault(p => p.id == id);
            if (element != null)
            {
                _invoiceStatuses.Remove(element);
                return true;
            }
            return false;
        }
        #endregion

        #region MailStatuses Get-Set
        public static IQueryable<motskin_mailStatuses> GetMailStatuses()
        {
            return _mailStatuses.AsQueryable();
        }
        public static int SaveMailStatuses(motskin_mailStatuses element, bool withSave = true)
        {
            if (element.id == 0)
            {
                element.id = _mailStatuses.Count + 1;
                _mailStatuses.Add(element);
            }
            else
            {
                int pos = _mailStatuses.IndexOf(_mailStatuses.FirstOrDefault(p => p.id == element.id));
                if (pos >= 0)
                    _mailStatuses[pos] = element;
            }
            return _mailStatuses.Count;
        }
        public static bool DeleteMailStatuses(int id)
        {
            motskin_mailStatuses element = _mailStatuses.FirstOrDefault(p => p.id == id);
            if (element != null)
            {
                _mailStatuses.Remove(element);
                return true;
            }
            return false;
        }
        #endregion

        #region Contractors Get-Set
        public static IQueryable<motskin_contractors> GetContractors()
        {
            return _contractors.AsQueryable();
        }

        public static int SaveContractor(motskin_contractors element, bool withSave = true)
        {
            if (element.id == 0)
            {
                element.id = _contractors.Count + 1;
                _contractors.Add(element);
            }
            else
            {
                int pos = _contractors.IndexOf(_contractors.FirstOrDefault(p => p.id == element.id));
                if (pos >= 0)
                    _contractors[pos] = element;
            }
            return _contractors.Count;
        }
        public static bool DeleteContractor(int id)
        {
            motskin_contractors element = _contractors.FirstOrDefault(p => p.id == id);
            if (element != null)
            {
                _contractors.Remove(element);
                return true;
            }
            return false;
        }
        #endregion

        #region Documents Get-Set
        public static IQueryable<motskin_documents> GetDocuments()
        {
            return _documents.AsQueryable();
        }

        public static int SaveDocument(motskin_documents element, bool withSave = true)
        {
            element.motskin_documentStatuses = GetDocumentStatuses().FirstOrDefault(p => p.id == element.documentStatusID);
            element.motskin_documentTypes = GetDocumentTypes().FirstOrDefault(p => p.id == element.documentTypeID);
            element.motskin_contractors = GetContractors().FirstOrDefault(p => p.id == element.contractorID);

            if (element.id == 0)
            {
                element.id = _documents.Count+1;
                 _documents.Add(element);
            }
            else
            {
                int pos = _documents.IndexOf(_documents.FirstOrDefault(p => p.id == element.id));
                if (pos >= 0)
                    _documents[pos] = element;
            }
            return _documents.Count;
        }
        public static bool DeleteDocument(int id)
        {
            motskin_documents element = _documents.FirstOrDefault(p => p.id == id);
            if (element !=null)
            {
                element.isDeleted = true;
                return true;
            }
            return false;
        }
        #endregion

        #region Invoices Get-Set
        public static IQueryable<motskin_invoices> GetInvoices()
        {
            return _invoices.AsQueryable();
        }

        public static int SaveInvoice(motskin_invoices element, bool withSave = true)
        {
            element.motskin_invoiceStatuses = GetInvoiceStatuses().FirstOrDefault(p => p.id == element.invoiceStatusID);
            element.motskin_contractors = GetContractors().FirstOrDefault(p => p.id == element.contractorID);

            if (element.id == 0)
            {
                element.id = _invoices.Count + 1;
                _invoices.Add(element);
            }
            else
            {
                int pos = _invoices.IndexOf(_invoices.FirstOrDefault(p => p.id == element.id));
                if (pos >= 0)
                    _invoices[pos] = element;
            }
            return _invoices.Count;
        }
        public static bool DeleteInvoice(int id)
        {
            motskin_invoices element = _invoices.FirstOrDefault(p => p.id == id);
            if (element != null)
            {
                element.isDeleted = true;
                return true;
            }
            return false;
        }
        #endregion

        #region Mails Get-Set
        public static IQueryable<motskin_mails> GetMails()
        {
            return _mails.AsQueryable();
        }

        public static int SaveMail(motskin_mails element, bool withSave = true)
        {
            element.motskin_mailStatuses = GetMailStatuses().FirstOrDefault(p => p.id == element.mailStatusID);
            element.motskin_sendSystems = GetSendSystems().FirstOrDefault(p => p.id == element.sendSystemID);

            if (element.id == 0)
            {
                element.id = _mails.Count + 1;
                _mails.Add(element);
            }
            else
            {
                int pos = _mails.IndexOf(_mails.FirstOrDefault(p => p.id == element.id));
                if (pos >= 0)
                    _mails[pos] = element;
            }
            return _mails.Count;
        }
        public static bool DeleteMail(int id)
        {
            motskin_mails element = _mails.FirstOrDefault(p => p.id == id);
            if (element != null)
            {
                element.isDeleted = true;
                return true;
            }
            return false;
        }
        #endregion

        #region Status Log Get-Set

        public static IQueryable<motskin_documentStatusLog> GetDocumentStatusLog()
        {
            return _documentStatusLog.AsQueryable();
        }

        public static int SaveDocumentStatusLog(motskin_documentStatusLog element, bool withSave = true)
        {
            element.motskin_documents = GetDocuments().FirstOrDefault(p => p.id == element.documentID);
            element.motskin_documentStatuses = GetDocumentStatuses().FirstOrDefault(p => p.id == element.documentStatusID);

            if (element.id == 0)
            {
                element.id = _documentStatusLog.Count + 1;
                _documentStatusLog.Add(element);
            }
            else
            {
                int pos = _documentStatusLog.IndexOf(_documentStatusLog.FirstOrDefault(p => p.id == element.id));
                if (pos >= 0)
                    _documentStatusLog[pos] = element;
            }
            return _documentStatusLog.Count;
        }

        public static IQueryable<motskin_invoiceStatusLog> GetInvoiceStatusLog()
        {
            return _invoiceStatusLog.AsQueryable();
        }

        public static int SaveInvoiceStatusLog(motskin_invoiceStatusLog element, bool withSave = true)
        {
            element.motskin_invoices = GetInvoices().FirstOrDefault(p => p.id == element.invoiceID);
            element.motskin_invoiceStatuses = GetInvoiceStatuses().FirstOrDefault(p => p.id == element.invoiceStatusID);

            if (element.id == 0)
            {
                element.id = _invoiceStatusLog.Count + 1;
                _invoiceStatusLog.Add(element);
            }
            else
            {
                int pos = _invoiceStatusLog.IndexOf(_invoiceStatusLog.FirstOrDefault(p => p.id == element.id));
                if (pos >= 0)
                    _invoiceStatusLog[pos] = element;
            }
            return _invoiceStatusLog.Count;
        }

        public static IQueryable<motskin_mailStatusLog> GetMailStatusLog()
        {
            return _mailStatusLog.AsQueryable();
        }

        public static int SaveMailStatusLog(motskin_mailStatusLog element, bool withSave = true)
        {
            element.motskin_mails = GetMails().FirstOrDefault(p => p.id == element.mailID);
            element.motskin_mailStatuses = GetMailStatuses().FirstOrDefault(p => p.id == element.mailStatusID);

            if (element.id == 0)
            {
                element.id = _mailStatusLog.Count + 1;
                _mailStatusLog.Add(element);
            }
            else
            {
                int pos = _mailStatusLog.IndexOf(_mailStatusLog.FirstOrDefault(p => p.id == element.id));
                if (pos >= 0)
                    _mailStatusLog[pos] = element;
            }
            return _mailStatusLog.Count;
        }
        #endregion
    }
}