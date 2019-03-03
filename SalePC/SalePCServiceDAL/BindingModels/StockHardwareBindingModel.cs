using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalePCServiceDAL.BindingModels
{
    public class StockHardwareBindingModel
    {
        public int Id { get; set; }
        public int StockId { get; set; }
        public int HardwareId { get; set; }
        public int Count { get; set; }
    }

}
