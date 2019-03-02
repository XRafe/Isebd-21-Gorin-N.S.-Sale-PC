using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalePCServiceDAL.ViewModels
{
    public class StockViewModel
    {
        public int Id { get; set; }
        public string StockName { get; set; }
        public List<StockHardwareViewModel> StockHardwares { get; set; }
    }
}
