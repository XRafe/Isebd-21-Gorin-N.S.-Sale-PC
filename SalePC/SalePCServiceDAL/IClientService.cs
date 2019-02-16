using System;
using SalePCServiceDAL.BindingModels;
using SalePCServiceDAL.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalePCServiceDAL.Interfaces
{
    public interface IClientService
    {
        List<ClientViewModel> GetList();
        ClientViewModel GetElement(int id);
        void AddElement(ClientBindingModel model);
        void UpdElement(ClientBindingModel model);
        void DelElement(int id);
    }

}
