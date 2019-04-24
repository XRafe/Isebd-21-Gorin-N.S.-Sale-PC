using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SalePCServiceDAL.BindingModels
{
    [DataContract]
    public class PCBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string PCName { get; set; }
        [DataMember]
        public decimal Price { get; set; }
        public List<PCHardwareBindingModel> PCHardwares { get; set; }
    }
}
