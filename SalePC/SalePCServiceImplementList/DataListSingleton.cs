using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalePC;

namespace SalePCServiceImplementList
{
    class DataListSingleton
    {
        private static DataListSingleton instance;
        public List<Client> Clients { get; set; }
        public List<Hardware> Hardwares { get; set; }
        public List<Order> Orders { get; set; }
        public List<Computer> Computers { get; set; }
        public List<ComputerHardware> ComputerHardware { get; set; }
        private DataListSingleton()
        {
            Clients = new List<Client>();
            Hardwares = new List<Hardware>();
            Orders = new List<Order>();
            Computers = new List<Computer>();
            ComputerHardware = new List<ComputerHardware>();
        }
        public static DataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataListSingleton();
            }
            return instance;
        }
    }
}
