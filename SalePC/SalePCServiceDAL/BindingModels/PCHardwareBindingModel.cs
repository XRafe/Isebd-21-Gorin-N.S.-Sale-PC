using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalePCServiceDAL.BindingModels
{
    public class PCHardwareBindingModel
    {
        public int Id { get; set; }
        public int PCId { get; set; }
        public int HardwareId { get; set; }
        public int Count { get; set; }
    }
}
