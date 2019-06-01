using System.Runtime.Serialization;

namespace SalePCServiceDAL.BindingModels
{
    [DataContract]
    public class ClientBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string ClientFIO { get; set; }
    }
}
