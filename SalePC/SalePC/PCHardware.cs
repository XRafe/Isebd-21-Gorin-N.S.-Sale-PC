using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalePC
{
    /// <summary>
    /// Сколько компонентов, требуется при изготовлении изделия
    /// </summary>
    public class PCHardware
    {
        public int Id { get; set; }
        public int PCId { get; set; }
        public int HardwareId { get; set; }
        public int Count { get; set; }
    }
}
