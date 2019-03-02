using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalePCServiceDAL.BindingModels;
using SalePCServiceDAL.ViewModels;

namespace SalePCServiceDAL.Interfaces
{
    public interface IPCService
    {
        List<PCViewModel> GetList();
        PCViewModel GetElement(int id);
        void AddElement(PCBindingModel model);
        void UpdElement(PCBindingModel model);
        void DelElement(int id);
    }
}
