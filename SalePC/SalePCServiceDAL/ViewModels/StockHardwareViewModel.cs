using System.ComponentModel;
using System.Runtime.Serialization;


namespace SalePCServiceDAL.ViewModels
{
    [DataContract]
    public class StockHardwareViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int StockId { get; set; }
        [DataMember]
        public int HardwareId { get; set; }
        [DisplayName("Название компонента")]
        [DataMember]
        public string HardwareName { get; set; }
        [DisplayName("Количество")]
        [DataMember]
        public int Count { get; set; }
    }

}
