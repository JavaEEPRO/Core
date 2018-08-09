using arkAS.Areas.Tsilurik.BLL;
using System.Web.Mvc;

namespace arkAS.Areas.Tsilurik.Controllers
{
    public class BaseController : Controller
    {
        protected IManager mng;

        public BaseController(IManager mng)
        {
            this.mng = mng;
        }

    }
}