using System.Runtime.Serialization;

namespace SalePCServiceDAL.BindingModels
{
    [DataContract]
    public class StockHardwareBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int StockId { get; set; }
        [DataMember]
        public int HardwareId { get; set; }
        [DataMember]
        public int Count { get; set; }
    }

}
