using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VehicleService.Models;
using VehicleService.Repository;

namespace VehicleService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(VehicleController));
        private readonly IVehicleRepository _IVehicleRepository;
        public VehicleController(IVehicleRepository Repo)
        {
            this._IVehicleRepository = Repo;
        }

        [HttpGet]

        public IActionResult GetAllVehicles()
        {
            try
            {
                _log4net.Info(" Http GET is accesed");
                IEnumerable<VehicleModel> Vlist = _IVehicleRepository.GetAll();
                return Ok(Vlist);
            }
            catch
            {
                _log4net.Error("Error in Get request");
                return new NoContentResult();
            }
            
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                _log4net.Info(" Http GET by is accesed " + id);
                var Vlist = _IVehicleRepository.GetById(id);
                return new OkObjectResult(Vlist);
            }
            catch
            {
                _log4net.Error("Error in Get by id Request");
                return new NoContentResult();
            }
           
        }
    }
}
