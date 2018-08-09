using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using arkAS.Areas.Motskin.BLL;

namespace arkAS.Areas.Motskin.Controllers
{
    public abstract class BaseController : Controller
    {
        protected IManager mng;
        protected BaseController(IManager mng)
        {
            this.mng = mng;
        }
        protected override void Dispose(bool disposing)
        {
            if (mng != null)
                mng.Dispose();
        }

        /// <summary>
        /// Преобразует сложную структуру данных, получаемых с CRUD, в простой одноуровневый Dictionary
        /// </summary>
        /// <param name="parameters">Получаемый с CRUD объект параметров, содержащий fielsd</param>
        /// <returns></returns>
        protected Dictionary<string, string> ConvertToSimpleDictionary(Dictionary<string, object> parameters)
        {
            Dictionary<string, string> res = new Dictionary<string, string>();
            var fields = (parameters["fields"] as ArrayList).ToArray().ToList().Select(x => x as Dictionary<string, object>).ToList();
            foreach (var el in fields)
            {
                string key = el["code"].ToString();
                string value = el["value"].ToString();
                res.Add(key, value);
            }
            return res;
        }
    }
}
