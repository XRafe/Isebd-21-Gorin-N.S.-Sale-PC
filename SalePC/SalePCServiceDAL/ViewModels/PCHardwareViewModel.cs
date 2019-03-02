using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalePCServiceDAL.ViewModels
{
    public class PCHardwareViewModel
    {
        public int Id { get; set; }
        public int PCId { get; set; }
        public int HardwareId { get; set; }
        public int Count { get; set; }
        public string HardwareNames { get; set; }
    }
}
