using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleService.Models;

namespace VehicleService.Repository
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly VehicleDbContext dbContext;
        public VehicleRepository(VehicleDbContext _dbContext)
        {
            this.dbContext = _dbContext;
        }
        public IEnumerable<VehicleModel> GetAll()
        {
            var vehiclelist = dbContext.Vehicles.ToList();
            return vehiclelist;
        }

        public VehicleModel GetById(int vehicle_id)
        {
            return dbContext.Vehicles.FirstOrDefault(v=>v.Vehicle_Id == vehicle_id);
            
        }
    }
}
