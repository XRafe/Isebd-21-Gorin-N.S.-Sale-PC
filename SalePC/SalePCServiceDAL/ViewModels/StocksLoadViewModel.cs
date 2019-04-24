using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SalePCServiceDAL.ViewModels
{
    [DataContract]
    public class StocksLoadViewModel
    {
        [DataMember]
        public string StockName { get; set; }
        [DataMember]
        public int TotalCount { get; set; }
        public IEnumerable<Tuple<string, int>> Hardwares { get; set; }
    }
}
