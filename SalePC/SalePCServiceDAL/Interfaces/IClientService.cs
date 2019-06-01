using SalePCServiceDAL.Attributies;
using SalePCServiceDAL.BindingModels;
using SalePCServiceDAL.ViewModels;
using System.Collections.Generic;

namespace SalePCServiceDAL.Interfaces
{
    [CustomInterface("Интерфейс для работы с клиентами")]
    public interface IClientService
    {
        [CustomMethod("Метод получения списка клиентов")]
        List<ClientViewModel> GetList();
        [CustomMethod("Метод получения клиента по id")]
        ClientViewModel GetElement(int id);
        [CustomMethod("Метод добавления клиента")]
        void AddElement(ClientBindingModel model);
        [CustomMethod("Метод изменения данных по клиенту")]
        void UpdElement(ClientBindingModel model);
        [CustomMethod("Метод удаления клиента")]
        void DelElement(int id);
    }
}