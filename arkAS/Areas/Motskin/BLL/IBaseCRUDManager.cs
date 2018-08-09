using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using arkAS.BLL;
using arkAS.Models;

namespace arkAS.Areas.Motskin.BLL
{
    /// <summary>
    /// базовый CRUD интерфейс для наследования от него интерфейсов менеджера
    /// </summary>
    /// <typeparam name="T">Документ, счёт или почта</typeparam>
    public interface IBaseCRUDManager<T>
    {
        dynamic GetItems(ASCRUDGetItemsModel parameters, aspnet_Users user, out string msg, out int total);
        T Get(int id, aspnet_Users user, out string msg);
        T Create(Dictionary<string, string> parameters, aspnet_Users user, out string msg);
        T Edit(Dictionary<string, string> parameters, aspnet_Users user, out string msg);
        bool Delete(int id, aspnet_Users user, out string msg);

    }
}
