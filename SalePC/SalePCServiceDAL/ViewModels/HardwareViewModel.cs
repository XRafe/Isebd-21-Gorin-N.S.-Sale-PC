﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace SalePCServiceDAL.ViewModels
{
    [DataContract]
    public class HardwareViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string HardwareName { get; set; }
    }
}
