using SalePCServiceDAL.BindingModels;
using SalePCServiceDAL.ViewModels;
using System.Collections.Generic;
namespace SalePCServiceDAL.Interfaces
{
    public interface IImplementerService
    {
        List<ImplementerViewModel> GetList();
        ImplementerViewModel GetElement(int id);
        void AddElement(ImplementerBindingModel model);
        void UpdElement(ImplementerBindingModel model);
        void DelElement(int id);
        ImplementerViewModel GetFreeWorker();
    }
}
