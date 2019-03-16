using SalePCServiceDAL.Interfaces;
using SalePCServiceImplementDataBase;
using SalePCServiceImplementDataBase.Implementations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;

namespace SalePCView
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormMain>());
        }
        public static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<DbContext, AbstractPCDbContext>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IClientService, ClientServiceDB>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IHardwareService, HardwareServiceDB>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IPCService, PCServiceDB>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStockService, StockServiceDB>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMainService, MainServiceDB>(new
           HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}
