using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalePC;
using SalePCServiceDAL.BindingModels;
using SalePCServiceDAL.Interfaces;
using SalePCServiceDAL.ViewModels;

namespace SalePCServiceImplementDataBase.Implementations
{
    public class StockServiceDB : IStockService
    {
        private AbstractPCDbContext context;
        public StockServiceDB(AbstractPCDbContext context)
        {
            this.context = context;
        }
        public List<StockViewModel> GetList()
        {
            List<StockViewModel> result = context.Stocks.Select(rec => new
           StockViewModel
            {
                Id = rec.Id,
                StockName = rec.StockName,
                StockHardware = context.StockHardwares
            .Where(recPC => recPC.StockId == rec.Id)
           .Select(recPC => new StockHardwareViewModel
           {
               Id = recPC.Id,
               StockId = recPC.StockId,
               HardwareId = recPC.HardwareId,
               HardwareName = recPC.Hardware.HardwareName,
               Count = recPC.Count
           })
           .ToList()
            })
            .ToList();
            return result;
        }
        public StockViewModel GetElement(int id)
        {
            Stock element = context.Stocks.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new StockViewModel
                {
                    Id = element.Id,
                    StockName = element.StockName,
                    StockHardware = context.StockHardwares
 .Where(recStock => recStock.StockId == element.Id)
 .Select(recStock => new StockHardwareViewModel
 {
     Id = recStock.Id,
     StockId = recStock.StockId,
     HardwareId = recStock.HardwareId,
     HardwareName = recStock.Hardware.HardwareName,
     Count = recStock.Count
 })
 .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(StockBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Stock element = context.Stocks.FirstOrDefault(rec =>
                   rec.StockName == model.StockName);
                    if (element != null)
                    {
                        throw new Exception("Уже есть изделие с таким названием");
                    }
                    element = new Stock
                    {
                        StockName = model.StockName
                    };
                    context.Stocks.Add(element);
                    context.SaveChanges();
                    // убираем дубли по компонентам
                    var groupHardwares = model.StockHardwares
                     .GroupBy(rec => rec.HardwareId)
                    .Select(rec => new
                    {
                        HardwareId = rec.Key,
                        Count = rec.Sum(r => r.Count)
                    });
                    // добавляем компоненты
                    foreach (var groupHardware in groupHardwares)
                    {
                        context.StockHardwares.Add(new StockHardware
                        {
                            StockId = element.Id,
                            HardwareId = groupHardware.HardwareId,
                            Count = groupHardware.Count
                        });
                        context.SaveChanges();
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public void UpdElement(StockBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Stock element = context.Stocks.FirstOrDefault(rec =>
                   rec.StockName == model.StockName && rec.Id != model.Id);
                    if (element != null)
                    {
                        throw new Exception("Уже есть изделие с таким названием");
                    }
                    element = context.Stocks.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    element.StockName = model.StockName;
                    context.SaveChanges();
                    // обновляем существуюущие компоненты
                    var compIds = model.StockHardwares.Select(rec =>
                   rec.HardwareId).Distinct();
                    var updateHardwares = context.StockHardwares.Where(rec =>
                   rec.StockId == model.Id && compIds.Contains(rec.HardwareId));
                    foreach (var updateHardware in updateHardwares)
                    {
                        updateHardware.Count =
                       model.StockHardwares.FirstOrDefault(rec => rec.Id == updateHardware.Id).Count;
                    }
                    context.SaveChanges();
                    context.StockHardwares.RemoveRange(context.StockHardwares.Where(rec =>
                    rec.StockId == model.Id && !compIds.Contains(rec.HardwareId)));
                    context.SaveChanges();
                    // новые записи
                    var groupHardwares = model.StockHardwares
                    .Where(rec => rec.Id == 0)
                   .GroupBy(rec => rec.HardwareId)
                   .Select(rec => new
                   {
                       HardwareId = rec.Key,
                       Count = rec.Sum(r => r.Count)
                   });
                    foreach (var groupHardware in groupHardwares)
                    {
                        StockHardware elementStock =
                       context.StockHardwares.FirstOrDefault(rec => rec.StockId == model.Id &&
                       rec.HardwareId == groupHardware.HardwareId);
                        if (elementStock != null)
                        {
                            elementStock.Count += groupHardware.Count;
                            context.SaveChanges();
                        }
                        else
                        {
                            context.StockHardwares.Add(new StockHardware
                            {
                                StockId = model.Id,
                                HardwareId = groupHardware.HardwareId,
                                Count = groupHardware.Count
                            });
                            context.SaveChanges();
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public void DelElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Stock element = context.Stocks.FirstOrDefault(rec => rec.Id ==
                   id);
                    if (element != null)
                    {
                        // удаяем записи по компонентам при удалении изделия
                        context.StockHardwares.RemoveRange(context.StockHardwares.Where(rec =>
                        rec.StockId == id));
                        context.Stocks.Remove(element);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Элемент не найден");
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
