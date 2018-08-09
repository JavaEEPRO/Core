using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using arkAS.BLL;

namespace arkAS.Areas.Motskin.BLL
{
    /// <summary>
    /// Базовый интерфейс для тех, кто будет поддерживать работу с состояниями
    /// </summary>
    /// <typeparam name="TS">статус</typeparam>
    /// <typeparam name="TL">логирование статуса</typeparam>
    public interface IBaseStatusUseManager<TS,TL>
    {
        bool ChangeStatus(int id, string value, string parametrName, aspnet_Users user, out string msg);
        List<TS> GetStatuses();
        List<TL> GetLogStatus(int id);
    }
}
