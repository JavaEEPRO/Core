using arkAS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Antlr.Runtime.Tree;
using RDL;

namespace arkAS.Areas.Mikhailova.BLL
{
    public class ContractsManager : IContractsManager
    {
        #region System

        private IRepository _db;
        private IManager _mng;
        private bool _disposed;

        public ContractsManager(IRepository db, IManager mng)
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

        private void _debug(Exception ex, Object parameters, string additions = "")
        {
            RDL.Debug.LogError(ex, additions, parameters);
        }

        #endregion

        #region Contracts

        private bool _IsCanUserChange(aspnet_Users user)
        {
            var res = false;

            if (user != null && user.UserName == "core-admin@mail.ru")
            {
                res = true;
            }
            return res;
        }

        public bool IsCanCurrentUserChange()
        {
            return _IsCanUserChange(_mng.GetUser());
        }

        public List<Mikhailova_contracts> GetContracts()
        {
            var res = new List<Mikhailova_contracts>();

            try
            {
                res = _db.GetContracts().ToList();
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
        }

        public Mikhailova_contracts GetContract(int id)
        {
            var res = new Mikhailova_contracts();
            try
            {
                res = _db.GetContracts().FirstOrDefault(x => x.id == id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { id }, "");
            }
            return res;
        }

        public void SaveContract(Mikhailova_contracts item)
        {
           try
            {
               _db.SaveContract(item);
            }
            catch (Exception ex)
            {
                _debug(ex, new { item }, "");
            }
        }

        public bool CreateContract(string name)
        {
            var res = false;
            var contract = new Mikhailova_contracts();
            try
            {
                contract = new Mikhailova_contracts { id = 0, desc = name };
                SaveContract(contract);
                res = true;
            }
            catch (Exception ex)
            {
                _debug(ex, new { name }, "");
            }
            return res;
        }

        public bool EditContract(aspnet_Users user, int id, string name, string value, out string msg)   
        {
            var res = false;
            var item = new Mikhailova_contracts();
            msg = "";
            try
            {
                 if (!_IsCanUserChange(user))
                {
                    msg = "Недостаточна прав для редактирования!";
                }
            
                else 
                {
                    item = GetContract(id);
                    if (item != null)
                    {
                        switch (name)
                        {
                            case "desc":
                                item.desc = value;
                                break;
                            case "number":
                                item.number = value;
                                break;
                            case "statusName":
                                if (value != "") item.statusID = RDL.Convert.StrToInt(value, 0);
                                break;
                            case "contagentName":
                                if (value != "") item.contagentID = RDL.Convert.StrToInt(value, 0);
                                break;
                        }
                        SaveContract(item);
                        res = true;
                        msg = "Успешно";
                    }
                }
                
            }
            catch (Exception ex)
            {
                _debug(ex, new { id, name }, "");
                msg = "Произошла ошибка, поле не изменено";
            }
            return res;
        }

        public bool DeleteContract(aspnet_Users user, int id, out string msg)
        {
            var res = false;
            try
            {
                if (!_IsCanUserChange(user))
                {
                    msg = "Недостаточна прав для удаления!";
                }
                else
                {
                    RDL.CacheManager.PurgeCacheItems("Mikhailova_contracts");
                    res = _db.DeleteContract(id);
                    msg = "Успешно";
                }
                
            }
            catch (Exception ex)
            {
                _debug(ex, new { id }, "id");
                msg = "Произошла ошибка, поле не изменено";
            }
            return res;

        }

        public bool EditContractField(aspnet_Users userRol, int id, string code, string value, out string msg, Mikhailova_contracts user)
        {
            bool res = false;
            var document = new Mikhailova_contracts();
            try
            {
                if (!_IsCanUserChange(userRol))
                {
                    msg = "Недостаточна прав для редактирования!";
                }
                else
                {
                    document = GetContract(id);

                    if (document != null)
                    {
                        //TODO
                        SaveContract(document);
                        res = true;
                        msg = "Успешно";
                    }
                    else
                        msg = "Не удалось найти документ";
                }
               
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
                msg = "Произошла ошибка, поле не изменено";
            }
            return res;
        }

        public bool EditContractStatusField(aspnet_Users user, int pk, string code, string value, out string msg)
        {
            bool res = false;
            var contractStatus = new Mikhailova_statuses_contracts();
            try
            {
                if (!_IsCanUserChange(user))
                {
                    msg = "Недостаточна прав для редактирования!";
                }
                else
                {
                    contractStatus = GetStatusContract(pk);

                    if (contractStatus != null)
                    {
                        switch (code)
                        {
                            case "name": contractStatus.name = value;
                                break;
                            case "code": contractStatus.code = value;
                                break;

                        }
                        SaveStatusContract(contractStatus);
                        res = true;
                        msg = "Успешно";
                    }
                    else
                        msg = "Не удалось найти статус документ"; 
                }
                
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
                msg = "Произошла ошибка, поле не изменено";
            }
            return res;
        }
        
        public List<Mikhailova_statuses_contracts> GetStatusesContract()
        {
            var res = new List<Mikhailova_statuses_contracts>();
            try
            {
                res = _db.GetStatusesContract().ToList();
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
        }

        public Mikhailova_statuses_contracts GetStatusContract(int id)
        {
            var res = new Mikhailova_statuses_contracts();
            var key = "gl_docStatuses" + id;
            if (CacheManager.EnableCaching && CacheManager.Cache[key] != null)
            {
                res = (Mikhailova_statuses_contracts)CacheManager.Cache[key];
            }
            else
            {
                try
                {
                    res = _db.GetStatusesContract().FirstOrDefault(x => x.id == id);
                    CacheManager.CacheData(key, res);
                }
                catch (Exception ex)
                {
                    _debug(ex, new { id }, "id");
                }
            }
            return res;
        }

        public int SaveStatusContract(Mikhailova_statuses_contracts item, bool withSave = true)
        {
            var res = 0;
            try
            {
                res = _db.SaveStatusContract(item);
            }
            catch (Exception ex)
            {
                _debug(ex, new { item }, "item");
            }
            return res;
        }

        public bool DeleteStatusContract(aspnet_Users user, int id, out string msg)
        {
            msg = "";
            var res = false;
            try
            {
                if (!_IsCanUserChange(user))
                {
                    msg = "Недостаточна прав для удаления!";
                }
                else
                {
                    RDL.CacheManager.PurgeCacheItems("Mikhailova_statuses_contracts");
                    res = _db.DeleteStatusContract(id);
                }
            }
            catch (Exception ex)
            {
                _debug(ex, new { id }, "id");
            }
            return res;
        }

        public bool EditDocumentStatusField(aspnet_Users user, int id, string code, string value, out string msg)
        {
            bool res = false;
            var documentStatus = new Mikhailova_statuses_contracts();
            try
            {
                if (!_IsCanUserChange(user))
                {
                    msg = "Недостаточна прав для редактирования!";
                }
                else
                {
                    documentStatus = GetStatusContract(id);

                    if (documentStatus != null)
                    {
                        switch (code)
                        {
                            case "name": documentStatus.name = value;
                                break;
                            case "code": documentStatus.code = value;
                                break;

                        }
                        SaveStatusContract(documentStatus);
                        res = true;
                        msg = "Успешно";
                    }
                    else
                        msg = "Не удалось найти статус документ"; 
                }
                
            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
                msg = "Произошла ошибка, поле не изменено";
            }
            return res;
        }

        #endregion

        #region DocTypes

        public Mikhailova_docTypes GetDocType(int id)
        {
            var res = new Mikhailova_docTypes();
           
           try
           {
               res = _db.GetDocTypes().FirstOrDefault(x => x.id == id);
           }
            catch (Exception ex)
            {
                _debug(ex, new { id }, "");
            }
            return res;
        }

        public List<Mikhailova_docTypes> GetDocTypes()
        {
            var res = new List<Mikhailova_docTypes>();
            try
            {
                res = _db.GetDocTypes().ToList();

            }
            catch (Exception ex)
            {
                _debug(ex, new { }, "");
            }
            return res;
           
        }

        public void SaveDocType(Mikhailova_docTypes item)
        {
            try
            {
                _db.SaveDocType(item);
            }
            catch (Exception ex)
            {
                _debug(ex, new { item }, "");
            }
        }

        //public void DeleteDocType(int id)
        //{
        //    try
        //    {
        //        _db.DeleteDocType(id);
        //    }
        //    catch (Exception ex)
        //    {
        //        _debug(ex, new { id }, "");
        //    }
        //}

        #endregion

        #region DocTypeTemplates

        public List<Mikhailova_docTypeTemplates> GetDocTypeTemplates()
        {
            var res = new List<Mikhailova_docTypeTemplates>();
            res = _db.GetDocTypeTemplates().ToList();
            return res;
        }

        public Mikhailova_docTypeTemplates GetDocTypeTemplate(int id)
        {
            var res = new Mikhailova_docTypeTemplates();
            try
            {
                res = _db.GetDocTypeTemplates().FirstOrDefault(x => x.id == id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { id }, "id");
            }
            return res;
        }
        
        public List<Mikhailova_docTypeTemplates> GetListTemplatesByType(int typeId)
        {
            var res = new List<Mikhailova_docTypeTemplates>();
            res = _db.GetListTemplatesByType(typeId);
            return res;
        }

        public void SaveDocTypeTemplate(Mikhailova_docTypeTemplates item)
        {
            try
            {
                _db.SaveDocTypeTemplate(item);
            }
            catch (Exception ex)
            {
                _debug(ex, new { item }, "");
            }
        }

        public void DeleteDocTypeTemplate(int id)
        {
            try
            {
                _db.DeleteDocTypeTemplate(id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { id }, "id");

            }
        }

        public void EditTemplateField(int pk, string name, string value)
        {
            var item = GetDocTypeTemplate(pk);
            switch (name)
            {
                case "name": item.name = value; break;
            }
            SaveDocTypeTemplate(item);
        }

        #endregion

        #region Contagents

        public List<Mikhailova_contagents> GetContagents()
        {
            var res = new List<Mikhailova_contagents>();
            try
            {
                res = _db.GetContagents().ToList(); 
            }
            catch (Exception ex)
            {
                _debug(ex, new {  }, "");
            }
            return res;
        }

        public Mikhailova_contagents GetContagent(int id)
        {
            var res = new Mikhailova_contagents();
            try
            {
                res = _db.GetContagents().FirstOrDefault(x => x.id == id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { id }, "");
                
            }
            return res;
        }
        
        public void SaveContagent(Mikhailova_contagents item)
        {
            try
            {
                _db.SaveContagent(item);
            }
            catch (Exception ex)
            {
                _debug(ex, new { item }, "");
            }
        }

        public void DeleteContagent(int id)
        {
            try
            {
                _db.DeleteContagent(id);
            }
            catch (Exception ex)
            {
                _debug(ex, new { id }, "");
            }
        }

        #endregion

    }
}