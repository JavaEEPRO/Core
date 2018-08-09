using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using arkAS.BLL;

namespace arkAS.Areas.Motskin.BLL
{
    public interface IMailManager:
        IBaseCRUDManager<motskin_mails>,
        IBaseStatusUseManager<motskin_mailStatuses, motskin_mailStatusLog>
    {
        List<motskin_sendSystems> GetSystems();
    }

}
