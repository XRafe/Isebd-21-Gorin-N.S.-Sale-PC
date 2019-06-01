using SalePCServiceDAL.BindingModels;
using SalePCServiceDAL.ViewModels;
using System.Collections.Generic;

namespace SalePCServiceDAL.Interfaces
{
    public interface IMessageInfoService
    {
        List<MessageInfoViewModel> GetList();
        MessageInfoViewModel GetElement(int id);
        void AddElement(MessageInfoBindingModel model);
    }
}
