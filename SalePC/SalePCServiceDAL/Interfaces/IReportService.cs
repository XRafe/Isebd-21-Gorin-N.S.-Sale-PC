using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalePCServiceDAL.BindingModels;
using SalePCServiceDAL.ViewModels;


namespace SalePCServiceDAL.Interfaces
{
    public interface IReportService
    {
        void SavePCPrice(ReportBindingModel model);
        List<StocksLoadViewModel> GetStocksLoad();
        void SaveStocksLoad(ReportBindingModel model);
        List<ClientOrdersModel> GetClientOrders(ReportBindingModel model);
        void SaveClientOrders(ReportBindingModel model);
    }

}
