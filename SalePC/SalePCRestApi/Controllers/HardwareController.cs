using SalePCServiceDAL.BindingModels;
using SalePCServiceDAL.Interfaces;
using System;
using System.Web.Http;
namespace SalePCRestApi.Controllers
{
    public class HardwareController : ApiController
    {
        private readonly IHardwareService _service;
        public HardwareController(IHardwareService service)
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
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var element = _service.GetElement(id);
            if (element == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(element);
        }
        [HttpPost]
        public void AddElement(HardwareBindingModel model)
        {
            _service.AddElement(model);
        }
        [HttpPost]
        public void UpdElement(HardwareBindingModel model)
        {
            _service.UpdElement(model);
        }
        [HttpPost]
        public void DelElement(HardwareBindingModel model)
        {
            _service.DelElement(model.Id);
        }
    }
}