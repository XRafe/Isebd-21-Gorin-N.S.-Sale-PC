﻿using System;
using System.Collections.Generic;

namespace SalePCServiceDAL.ViewModels
{
    public class StocksLoadViewModel
    {
        public string StockName { get; set; }
        public int TotalCount { get; set; }
        public IEnumerable<Tuple<string, int>> Hardwares { get; set; }
    }
}
