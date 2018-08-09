using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using arkAS.BLL;
using arkAS.BLL.Core;
using arkAS.Models;
using Dapper;
using System.Data;
using arkAS.Areas.Motskin.Infrastructura;

namespace arkAS.Areas.Motskin.BLL
{
    public class DocumentsManager: IDocumentsManager
    {
        
        #region System

        private IRepository _db;
        private IManager _mng;
        private bool _disposed;

        public DocumentsManager(IRepository db, IManager mng)
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

        private void _debug(Exception ex, Object parameters=null, string additions = "")
        {
            RDL.Debug.LogError(ex, additions, parameters);
        }

        private bool _IsCanUserChange(aspnet_Users user)
        {
            return  (user != null && user.UserName == StaticData.User);
        }

        public bool IsCanCurrentUserChange()
        {
            return _IsCanUserChange(_mng.GetUser());
        }
        #endregion

        #region Documents

        public dynamic GetItems(ASCRUDGetItemsModel parameters, aspnet_Users user, out string msg, out int total)
        {
            msg = "";
            total = 0;
            var number = "";
            var typeID = "";
            var strStatusIDs = "";
            var contractorID = "";
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
                    number = parameters.filter.ContainsKey("number") ? parameters.filter["number"].ToString() : "";
                    typeID = parameters.filter.ContainsKey("typeID") ? parameters.filter["typeID"].ToString() : "";
                    contractorID = parameters.filter.ContainsKey("contractorID") ? parameters.filter["contractorID"].ToString() : "";
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
                p.Add("number", number);
                p.Add("typeID", typeID);
                p.Add("strStatusIDs", strStatusIDs);
                p.Add("contractorID", contractorID);
                p.Add("createdMin", createdMin);
                p.Add("createdMax", createdMax);
                p.Add("sort1", sort1);
                p.Add("direction1", direction1);
                p.Add("page", parameters.page);
                p.Add("pageSize", parameters.pageSize);
                p.Add("total", dbType: DbType.Int32, direction: ParameterDirection.Output);
                dynamic res = rep.GetSQLData<dynamic>("motskin_GetDocuments", p, CommandType.StoredProcedure) as List<dynamic> ?? new List<dynamic>();

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

        public motskin_documents Get(int id, aspnet_Users user, out string msg)
        {
            motskin_documents res;
            msg = "";
            try
            {
                if (!_IsCanUserChange(user))
                {
                    msg = "Нет прав для данной операции";
                    return null;
                }
                res = _db.GetDocuments().FirstOrDefault(x => x.id == id);

            }
            catch (Exception ex)
            {
                _debug(ex, new { documentID = id, userName = user.UserName });
                res = null;
                msg = "Сбой при выполнении операции";
            }

            return res;
        }

        public motskin_documents Create(Dictionary<string, string> parameters, aspnet_Users user, out string msg)
        {
            motskin_documents res = null;
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
                res = new motskin_documents
                {
                    date = DateTime.Now,        // дата неизменна
                    documentStatusID = statusID,
                    createdUnique = guid,
                    //reference = "" // пока адрес не сформирован
                };
                _fillDocument(res, parameters);

                _db.SaveDocument(res);  // сохраняем в бд
                                        // и сразу же получаем из базы этот объект, чтобы узнать его id.
                res = _db.GetDocuments().FirstOrDefault(x => x.createdUnique == guid);
                _logChangeStatus(res, "Статус изменен " + user.UserName);
            }
            catch (Exception ex)
            {
                _debug(ex, new { userName = user.UserName });
                msg = "Сбой при выполнении операции";
            }

            return res;
        }



        public motskin_documents Edit(Dictionary<string, string> parameters, aspnet_Users user, out string msg)
        {
            motskin_documents res;
            msg = "";
            try
            {
                if (!_IsCanUserChange(user))
                {
                    msg = "Нет прав для данной операции";
                    return null;
                }
                int id = RDL.Convert.StrToInt(parameters["id"].ToString(), 0);

                res = _db.GetDocuments().FirstOrDefault(x => x.id == id);
                if (res != null)
                {
                    _fillDocument(res, parameters);

                    _db.SaveDocument(res);  // сохраняем в бд
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

        private void _fillDocument(motskin_documents item, Dictionary<string, string> parameters)
        {
            foreach (var key in parameters.Keys)
            {
                string value = parameters[key].ToString();
                switch (key)
                {
                    case "number":
                        item.number = value;
                        break;
                    case "sum":
                        item.sum = RDL.Convert.StrToDecimal(value, 0);
                        break;
                    case "contractorID":
                        item.contractorID = RDL.Convert.StrToInt(value, 0);
                        break;
                    case "documentTypeID":
                        item.documentTypeID = RDL.Convert.StrToInt(value, 0);
                        break;
                    case "comment":
                        item.comment = value;
                        break;
                    case "reference":
                        item.reference = value;
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
                RDL.CacheManager.PurgeCacheItems("motskin_documents");
                res = _db.DeleteDocument(id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { documentID = id });
                res = false;
                msg = "Сбой при выполнении операции";
            }

            return res;
        }

        public List<motskin_documentTypes> GetTypes()
        {
            var res = new List<motskin_documentTypes>();
            try
            {
                res = _db.GetDocumentTypes().ToList();
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

                var item = _db.GetDocuments().FirstOrDefault(x => x.id == id);

                switch (parametrName)
                {
                    case "documentStatusName":
                        int statusID=0;
                        if (value != "")
                            statusID = RDL.Convert.StrToInt(value, 0);
                        if (item.documentStatusID != statusID)  // проверка, если этот метод вызовет контроллер напрямую
                        {
                            item.documentStatusID = statusID;
                            _logChangeStatus(item, "Статус изменен " + user.UserName);
                            _db.SaveDocument(item);
                        }
                        res = true;
                        break;
                }


            }
            catch (Exception ex)
            {
                _debug(ex, new { documentID = id });
                res = false;
                msg = "Сбой при выполнении операции";
            }

            return res;
        }

        public List<motskin_documentStatuses> GetStatuses()
        {
            var res = new List<motskin_documentStatuses>();
            try
            {
                res = _db.GetDocumentStatuses().ToList();
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
        public List<motskin_documentStatusLog> GetLogStatus(int id)
        {
            var res = new List<motskin_documentStatusLog>();
            try
            {
                res = _db.GetDocumentStatusLog().Where(x=>x.documentID == id).ToList();
            }
            catch (Exception ex)
            {
                _debug(ex);
                res = null;
            }

            return res;
        }



        private bool _logChangeStatus(motskin_documents item, string note = "")
        {
            var res = false;
            try
            {
                _db.SaveDocumentStatusLog(new motskin_documentStatusLog
                {

                    created = DateTime.Now,
                    documentStatusID = item.documentStatusID,
                    documentID = item.id,
                    note = note
                });
                res = true;
            }
            catch (Exception ex)
            {
                _debug(ex, new { documentID = item.id, statusID = item.documentStatusID, note });
            }
            return res;
        }
#endregion
    }
}