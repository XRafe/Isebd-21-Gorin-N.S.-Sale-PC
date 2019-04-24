using System.Runtime.Serialization;

namespace SalePCServiceDAL.ViewModels
{
    [DataContract]
    public class ClientViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string ClientFIO { get; set; }
    }
}
