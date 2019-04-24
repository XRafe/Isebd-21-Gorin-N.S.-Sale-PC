using System.Runtime.Serialization;

namespace SalePCServiceDAL.ViewModels
{
    [DataContract]
    public class PCHardwareViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int PCId { get; set; }
        [DataMember]
        public int HardwareId { get; set; }
        [DataMember]
        public int Count { get; set; }
        [DataMember]
        public string HardwareNames { get; set; }
    }
}
