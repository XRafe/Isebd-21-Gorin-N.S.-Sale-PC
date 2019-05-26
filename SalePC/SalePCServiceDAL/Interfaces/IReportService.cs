using System.Collections.Generic;
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
