﻿namespace SalePCServiceDAL.ViewModels
{
    public class ClientOrdersModel
    {
        public string ClientName { get; set; }
        public string DateCreate { get; set; }
        public string PCName { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
        public string Status { get; set; }
    }
}
