using System.Runtime.Serialization;

namespace SalePCServiceDAL.BindingModels
{
    [DataContract]
    public class PCHardwareBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int PCId { get; set; }
        [DataMember]
        public int HardwareId { get; set; }
        [DataMember]
        public int Count { get; set; }
    }
}
