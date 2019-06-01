using System;
using System.Collections.Generic;
using System.Linq;
using SalePC;
using SalePCServiceDAL.BindingModels;
using SalePCServiceDAL.Interfaces;
using SalePCServiceDAL.ViewModels;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Net;
using System.Net.Mail;
using System.Configuration;

namespace SalePCServiceImplementDataBase.Implementations
{
    public class MainServiceDB : IMainService
    {
        private AbstractPCDbContext context;
        public MainServiceDB(AbstractPCDbContext context)
        {
            this.context = context;
        }
        public List<OrderViewModel> GetList()
        {
            List<OrderViewModel> result = context.Orders.Select(rec => new OrderViewModel
            {
                Id = rec.Id,
                ClientId = rec.ClientId,
                PCId = rec.PCId,
                ImplementerId = rec.ImplementerId,
                DateCreate = SqlFunctions.DateName("dd", rec.DateCreate) + " " +
            SqlFunctions.DateName("mm", rec.DateCreate) + " " +
            SqlFunctions.DateName("yyyy", rec.DateCreate),
                DateImplement = rec.DateImplement == null ? "" :
            SqlFunctions.DateName("dd",
           rec.DateImplement.Value) + " " +
            SqlFunctions.DateName("mm",
           rec.DateImplement.Value) + " " +
            SqlFunctions.DateName("yyyy",
           rec.DateImplement.Value),
                Status = rec.Status.ToString(),
                Count = rec.Count,
                Sum = rec.Sum,
                ClientFIO = rec.Client.ClientFIO,
                PCName = rec.PC.PCName,
                ImplementerName = rec.Implementer.ImplementerFIO
            })
            .ToList();
            return result;
        }
        public List<OrderViewModel> GetFreeOrders()
        {
            List<OrderViewModel> result = context.Orders
            .Where(x => x.Status == OrderStatus.Принят || x.Status ==
           OrderStatus.НедостаточноРесурсов)
            .Select(rec => new OrderViewModel
            {
                Id = rec.Id
            })
            .ToList();
            return result;
        }
        public void CreateOrder(OrderBindingModel model)
        {
            var order = new Order
            {
                ClientId = model.ClientId,
                PCId = model.PCId,
                DateCreate = DateTime.Now,
                Count = model.Count,
                Sum = model.Sum,
                Status = OrderStatus.Принят
            };
        context.Orders.Add(order);
            context.SaveChanges();
            var client = context.Clients.FirstOrDefault(x => x.Id == model.ClientId);
            SendEmail(client.Mail, "Оповещение по заказам", string.Format("Заказ №{0} от {1} создан успешно", order.Id, order.DateCreate.ToShortDateString()));
        }
        public void TakeOrderInWork(OrderBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                try
                {
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    if (element.Status != OrderStatus.Принят && element.Status !=
                    OrderStatus.НедостаточноРесурсов)
                    {
                        throw new Exception("Заказ не в статусе \"Принят\"");
                    }
                    var PCHardwares = context.PCHardwares.Include(rec =>
                    rec.Hardware).Where(rec => rec.PCId == element.PCId);
                    // списываем
                    foreach (var PCHardware in PCHardwares)
                    {
                        int countOnStocks = PCHardware.Count * element.Count;
                        var stockHardwares = context.StockHardwares.Where(rec =>
                       rec.HardwareId == PCHardware.HardwareId);
                        foreach (var stockHardware in stockHardwares)
                        {
                            // компонентов на одном слкаде может не хватать
                            if (stockHardware.Count >= countOnStocks)
                            {
                                stockHardware.Count -= countOnStocks;
                                countOnStocks = 0;
                                context.SaveChanges();
                                break;
                            }
                            else
                            {
                                countOnStocks -= stockHardware.Count;
                                stockHardware.Count = 0;
                                context.SaveChanges();
                            }
                        }
                        if (countOnStocks > 0)
                        {
                            throw new Exception("Не достаточно компонента " + PCHardware.Hardware.HardwareName + " требуется " + PCHardware.Count + ", не хватает " + countOnStocks);
                         }
                    }
                    element.ImplementerId = model.ImplementerId;
                    element.DateImplement = DateTime.Now;
                    element.Status = OrderStatus.Выполняется;
                    context.SaveChanges();
                    SendEmail(element.Client.Mail, "Оповещение по заказам",
                    string.Format("Заказ №{0} от {1} передеан в работу", element.Id,
                    element.DateCreate.ToShortDateString()));
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    element.Status = OrderStatus.НедостаточноРесурсов;
                    context.SaveChanges();
                    transaction.Commit();
                    throw;
                }
            }
        }
        public void FinishOrder(OrderBindingModel model)
        {
            Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != OrderStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            element.Status = OrderStatus.Готов;
            context.SaveChanges();
            SendEmail(element.Client.Mail, "Оповещение по заказам", string.Format("Заказ №{ 0} от { 1} передан на оплату", element.Id, element.DateCreate.ToShortDateString()));
        }
        public void PayOrder(OrderBindingModel model)
        {
            Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != OrderStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            element.Status = OrderStatus.Оплачен;
            context.SaveChanges();
            SendEmail(element.Client.Mail, "Оповещение по заказам", string.Format("Заказ №{ 0} от { 1} оплачен успешно", element.Id, element.DateCreate.ToShortDateString()));
        }
        public void PutHardwareOnStock(StockHardwareBindingModel model)
        {
            StockHardware element = context.StockHardwares.FirstOrDefault(rec =>
           rec.StockId == model.StockId && rec.HardwareId == model.HardwareId);
            if (element != null)
            {
                element.Count += model.Count;
            }
            else
            {
                context.StockHardwares.Add(new StockHardware
                {
                    StockId = model.StockId,
                    HardwareId = model.HardwareId,
                    Count = model.Count
                });
            }
            context.SaveChanges();
        }
        private void SendEmail(string mailAddress, string subject, string text)
        {
            MailMessage objMailMessage = new MailMessage();
            SmtpClient objSmtpClient = null;
            try
            {
                objMailMessage.From = new
               MailAddress(ConfigurationManager.AppSettings["MailLogin"]);
                objMailMessage.To.Add(new MailAddress(mailAddress));
                objMailMessage.Subject = subject;
                objMailMessage.Body = text;
                objMailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
                objMailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                objSmtpClient = new SmtpClient("smtp.gmail.com", 587);
                objSmtpClient.UseDefaultCredentials = false;
                objSmtpClient.EnableSsl = true;
                objSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                objSmtpClient.Credentials = new
               NetworkCredential(ConfigurationManager.AppSettings["MailLogin"],
               ConfigurationManager.AppSettings["MailPassword"]);
                objSmtpClient.Send(objMailMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objMailMessage = null;
                objSmtpClient = null;
            }
        }

    }
}