using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalePCServiceDAL.ViewModels
{
    public class PCViewModel
    {
        public int Id { get; set; }
        public string PCName { get; set; }
        public decimal Price { get; set; }
        public List<PCHardwareViewModel> PCHardwares { get; set; }
    }
}
