﻿using SalePCServiceDAL.BindingModels;
using SalePCServiceDAL.Interfaces;
using System;
using System.Web.Http;
namespace SalePCRestApi.Controllers
{
    public class MainController : ApiController
    {
        private readonly IMainService _service;
        public MainController(IMainService service)
        {
            _service = service;
        }
        [HttpGet]
        public IHttpActionResult GetList()
        {
            var list = _service.GetList();
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }
        [HttpPost]
        public void CreateOrder(OrderBindingModel model)
        {
            _service.CreateOrder(model);
        }
        [HttpPost]
        public void TakeOrderInWork(OrderBindingModel model)
        {
            _service.TakeOrderInWork(model);
        }
        [HttpPost]
        public void FinishOrder(OrderBindingModel model)
        {
            _service.FinishOrder(model);
        }
        [HttpPost]
        public void PayOrder(OrderBindingModel model)
        {
            _service.PayOrder(model);
        }
        [HttpPost]
        public void PutComponentOnStock(StockHardwareBindingModel model)
        {
            _service.PutHardwareOnStock(model);
        }
    }
}
