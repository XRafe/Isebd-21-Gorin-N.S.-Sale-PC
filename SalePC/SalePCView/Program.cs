using SalePCServiceDAL.Interfaces;
using SalePCServiceImplementList.Implementations;
using System;
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
            currentContainer.RegisterType<IClientService, ClientServiceList>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IHardwareService, HardwareServiceList>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IComputerService, ComputerServiceList>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMainService, MainServiceList>(new
           HierarchicalLifetimeManager());
            return currentContainer;
        }
    }

}
