using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace SalePCServiceDAL.ViewModels
{
    [DataContract]
    public class StockViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DisplayName("Название склада")]
        [DataMember]
        public string StockName { get; set; }
        public List<StockHardwareViewModel> StockHardware { get; set; }
    }

}
