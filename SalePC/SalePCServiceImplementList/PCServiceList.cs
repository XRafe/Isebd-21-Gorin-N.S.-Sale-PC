using System;
using SalePC;
using SalePCServiceDAL.BindingModels;
using SalePCServiceDAL.Interfaces;
using SalePCServiceDAL.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalePCServiceImplementList
{
    public class PCServiceList : IPCService
    {
        private DataListSingleton source;
        public PCServiceList()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<PCViewModel> GetList()
        {
            List<PCViewModel> result = source.PCs.Select(rec => new
            PCViewModel
            {
                Id = rec.Id,
                PCName = rec.PCName
            })
             .ToList();
            return result;
        }
        public PCViewModel GetElement(int id)
        {
            PC element = source.PCs.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new PCViewModel
                {
                    Id = element.Id,
                    PCName = element.PCName
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(PCBindingModel model)
        {
            PC element = source.PCs.FirstOrDefault(rec => rec.PCName
== model.PCName);
            if (element != null)
            {
                throw new Exception("Уже есть компонент с таким названием");
            }
            int maxId = source.PCs.Count > 0 ? source.PCs.Max(rec =>
           rec.Id) : 0;
            source.PCs.Add(new PC
            {
                Id = maxId + 1,
                PCName = model.PCName
            });
        }
        public void UpdElement(PCBindingModel model)
        {
            PC element = source.PCs.FirstOrDefault(rec => rec.PCName
 == model.PCName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть компонент с таким названием");
            }
            element = source.PCs.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.PCName = model.PCName;
        }
        public void DelElement(int id)
        {
            PC element = source.PCs.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                source.PCs.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
