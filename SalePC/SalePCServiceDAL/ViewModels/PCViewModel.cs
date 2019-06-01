using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SalePCServiceDAL.ViewModels
{
    [DataContract]
    public class PCViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string PCName { get; set; }
        [DataMember]
        public decimal Price { get; set; }
        [DataMember]
        public List<PCHardwareViewModel> PCHardwares { get; set; }
    }
}
