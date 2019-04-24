using SalePCServiceDAL.BindingModels;
using SalePCServiceDAL.Interfaces;
using System;
using System.Web.Http;
namespace SalePCRestApi.Controllers
{
    public class PCController : ApiController
    {
        private readonly IPCService _service;
        public PCController(IPCService service)
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
        public void AddElement(PCBindingModel model)
        {
            _service.AddElement(model);
        }
        [HttpPost]
        public void UpdElement(PCBindingModel model)
        {
            _service.UpdElement(model);
        }
        [HttpPost]
        public void DelElement(PCBindingModel model)
        {
            _service.DelElement(model.Id);
        }
    }
}