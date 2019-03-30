using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalePC
{
    /// <summary>
    /// Сколько компонентов хранится на складе
    /// </summary>
    public class StockHardware
    {
        public int Id { get; set; }
        public int StockId { get; set; }
        public int HardwareId { get; set; }
        [Required]
        public int Count { get; set; }
        public virtual Hardware Hardware { get; set; }
        public virtual PC PC { get; set; }

    }
}
