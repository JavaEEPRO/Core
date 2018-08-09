using arkAS.Areas.Molchanov.BLL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace arkAS.Areas.Molchanov.Controllers
{
    public class BaseController : Controller
    {
        protected IManager mng;
        
        public BaseController (IManager mng)
        {
            this.mng = mng;
        }

        protected override void Dispose (bool disposing)
        {
            if (mng != null) mng.Dispose();
        }

        protected Dictionary<string, string> CRUDToDictionary(Dictionary<string, object> parameters)
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