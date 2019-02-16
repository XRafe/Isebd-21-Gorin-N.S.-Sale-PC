using System;
using SalePC;
using SalePCServiceDAL.BindingModels;
using SalePCServiceDAL.Interfaces;
using SalePCServiceDAL.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalePCServiceImplementList.Implementations
{
    public class ComputerServiceList : IComputerService
    {
        private DataListSingleton source;
        public ComputerServiceList()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<ComputerViewModel> GetList()
        {
            List<ComputerViewModel> result = new List<ComputerViewModel>();
            for (int i = 0; i < source.Computers.Count; ++i)
            {
                // требуется дополнительно получить список компонентов для изделия и их
                количество
            List<ComputerHardwareViewModel> ComputerHardware = new
List<ComputerHardwareViewModel>();
                for (int j = 0; j < source.ComputerHardware.Count; ++j)
                {
                    if (source.ComputerHardware[j].ComputerId == source.Computers[i].Id)
                    {
                        string HardwareName = string.Empty;
                        for (int k = 0; k < source.Hardwares.Count; ++k)
                        {
                            if (source.ComputerHardware[j].HardwareId ==
                           source.Hardwares[k].Id)
                            {
                                HardwareName = source.Hardwares[k].HardwareName;
                                break;
                            }
                        }
                        ComputerHardware.Add(new ComputerHardwareViewModel
                        {
                            Id = source.ComputerHardware[j].Id,
                            ComputerId = source.ComputerHardware[j].ComputerId,
                            HardwareId = source.ComputerHardware[j].HardwareId,
                            HardwareName = HardwareName,
                            Count = source.ComputerHardware[j].Count
                        });
                    }
                }
                result.Add(new ComputerViewModel
                {
                    Id = source.Computers[i].Id,
                    ComputerName = source.Computers[i].ComputerName,
                    Price = source.Computers[i].Price,
                    ComputerHardware = ComputerHardware
                });
            }
            return result;
        }
        public ComputerViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Computers.Count; ++i)
            {
                // требуется дополнительно получить список компонентов для изделия и их
                количество
            List<ComputerHardwareViewModel> ComputerHardware = new
List<ComputerHardwareViewModel>();
                for (int j = 0; j < source.ComputerHardware.Count; ++j)
                {
                    if (source.ComputerHardware[j].ComputerId == source.Computers[i].Id)
                    {
                        string HardwareName = string.Empty;
                        for (int k = 0; k < source.Hardwares.Count; ++k)
                        {
                            if (source.ComputerHardware[j].HardwareId ==
                           source.Hardwares[k].Id)
                            {
                                HardwareName = source.Hardwares[k].HardwareName;
                                break;
                            }
                        }
                        ComputerHardware.Add(new ComputerHardwareViewModel
                        {
                            Id = source.ComputerHardware[j].Id,
                            ComputerId = source.ComputerHardware[j].ComputerId,
                            HardwareId = source.ComputerHardware[j].HardwareId,
                            HardwareName = HardwareName,
                            Count = source.ComputerHardware[j].Count
                        });
                    }
                }
                if (source.Computers[i].Id == id)
                {
                    return new ComputerViewModel
                    {
                        Id = source.Computers[i].Id,
                        ComputerName = source.Computers[i].ComputerName,
                        Price = source.Computers[i].Price,
                        ComputerHardware = ComputerHardware
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(ComputerBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Computers.Count; ++i)
            {
                if (source.Computers[i].Id > maxId)
                {
                    maxId = source.Computers[i].Id;
                }
                if (source.Computers[i].ComputerName == model.ComputerName)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
            }
            source.Computers.Add(new Computer
            {
                Id = maxId + 1,
                ComputerName = model.ComputerName,
                Price = model.Price
            });
            // компоненты для изделия
            int maxPCId = 0;
            for (int i = 0; i < source.ComputerHardware.Count; ++i)
            {
                if (source.ComputerHardware[i].Id > maxPCId)
                {
                    maxPCId = source.ComputerHardware[i].Id;
                }
            }
            // убираем дубли по компонентам
            for (int i = 0; i < model.ComputerHardware.Count; ++i)
            {
                for (int j = 1; j < model.ComputerHardware.Count; ++j)
                {
                    if (model.ComputerHardware[i].HardwareId ==
                    model.ComputerHardware[j].HardwareId)
                    {
                        model.ComputerHardware[i].Count +=
                        model.ComputerHardware[j].Count;
                        model.ComputerHardware.RemoveAt(j--);
                    }
                }
            }
            // добавляем компоненты
            for (int i = 0; i < model.ComputerHardware.Count; ++i)
            {
                source.ComputerHardware.Add(new ComputerHardware
                {
                    Id = ++maxPCId,
                    ComputerId = maxId + 1,
                    HardwareId = model.ComputerHardware[i].HardwareId,
                    Count = model.ComputerHardware[i].Count
                });
            }
        }
        public void UpdElement(ComputerBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Computers.Count; ++i)
            {
                if (source.Computers[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Computers[i].ComputerName == model.ComputerName &&
                source.Computers[i].Id != model.Id)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Computers[index].ComputerName = model.ComputerName;
            source.Computers[index].Price = model.Price;
            int maxPCId = 0;
            for (int i = 0; i < source.ComputerHardware.Count; ++i)
            {
                if (source.ComputerHardware[i].Id > maxPCId)
                {
                    maxPCId = source.ComputerHardware[i].Id;
                }
            }
            // обновляем существуюущие компоненты
            for (int i = 0; i < source.ComputerHardware.Count; ++i)
            {
                if (source.ComputerHardware[i].ComputerId == model.Id)
                {
                    bool flag = true;
                    for (int j = 0; j < model.ComputerHardware.Count; ++j)
                    {
                        // если встретили, то изменяем количество
                        if (source.ComputerHardware[i].Id ==
                       model.ComputerHardware[j].Id)
                        {
                            source.ComputerHardware[i].Count =
                           model.ComputerHardware[j].Count;
                            flag = false;
                            break;
                        }
                    }
                    // если не встретили, то удаляем
                    if (flag)
                    {
                        source.ComputerHardware.RemoveAt(i--);
                    }
                }
            }
            // новые записи
            for (int i = 0; i < model.ComputerHardware.Count; ++i)
            {
                if (model.ComputerHardware[i].Id == 0)
                {
                    // ищем дубли
                    for (int j = 0; j < source.ComputerHardware.Count; ++j)
                    {
                        if (source.ComputerHardware[j].ComputerId == model.Id &&
                        source.ComputerHardware[j].HardwareId ==
                       model.ComputerHardware[i].HardwareId)
                        {
                            source.ComputerHardware[j].Count +=
                           model.ComputerHardware[i].Count;
                            model.ComputerHardware[i].Id =
                           source.ComputerHardware[j].Id;
                            break;
                        }
                    }
                    // если не нашли дубли, то новая запись
                    if (model.ComputerHardware[i].Id == 0)
                    {
                        source.ComputerHardware.Add(new ComputerHardware
                        {
                            Id = ++maxPCId,
                            ComputerId = model.Id,
                            HardwareId = model.ComputerHardware[i].HardwareId,
                            Count = model.ComputerHardware[i].Count
                        });
                    }
                }
            }
        }
        public void DelElement(int id)
        {
            // удаяем записи по компонентам при удалении изделия
            for (int i = 0; i < source.ComputerHardware.Count; ++i)
            {
                if (source.ComputerHardware[i].ComputerId == id)
                {
                    source.ComputerHardware.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Computers.Count; ++i)
            {
                if (source.Computers[i].Id == id)
                {
                    source.Computers.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }

}
