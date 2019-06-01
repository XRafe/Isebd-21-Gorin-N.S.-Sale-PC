using System.Runtime.Serialization;

namespace SalePCServiceDAL.BindingModels
{
    [DataContract]
    public class HardwareBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string HardwareName { get; set; }
    }
}
