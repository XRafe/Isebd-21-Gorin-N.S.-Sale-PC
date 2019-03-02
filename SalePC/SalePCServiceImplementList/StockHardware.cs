using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalePCServiceImplementList
{
    /// <summary>
    /// Сколько компонентов хранится на складе
    /// </summary>
    public class StockHardware
    {
        public int Id { get; set; }
        public int StockId { get; set; }
        public int HardwareId { get; set; }
        public int Count { get; set; }
    }

}
