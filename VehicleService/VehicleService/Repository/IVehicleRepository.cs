using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleService.Models;

namespace VehicleService.Repository
{
    public interface IVehicleRepository
    {
        IEnumerable<VehicleModel> GetAll();
        VehicleModel GetById(int Vehicle_id);
    }
}
