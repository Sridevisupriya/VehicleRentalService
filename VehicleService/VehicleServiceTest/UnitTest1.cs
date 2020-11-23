using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using VehicleService.Models;
using VehicleService.Repository;

namespace VehicleServiceTest
{
    public class Tests
    {

        List<VehicleModel> vehicle = new List<VehicleModel>();
        IQueryable<VehicleModel> vehicledata;
        Mock<DbSet<VehicleModel>> mockSet;
        Mock<VehicleDbContext> vehiclecontextmock;
        [SetUp]
        public void Setup()
        {
            vehicle = new List<VehicleModel>()
            {
                new VehicleModel{Vehicle_Id = 1, Vehicle_Cmpy="Hyndai", Rent_per_month=5000,Status=1},
                new VehicleModel{Vehicle_Id = 2, Vehicle_Cmpy="Suzuki", Rent_per_month=1500,Status=0}, 
                new VehicleModel{Vehicle_Id = 3, Vehicle_Cmpy="Volvo", Rent_per_month=2000,Status=1},
            };
            vehicledata = vehicle.AsQueryable();
            mockSet = new Mock<DbSet<VehicleModel>>();
            mockSet.As<IQueryable<VehicleModel>>().Setup(m => m.Provider).Returns(vehicledata.Provider);
            mockSet.As<IQueryable<VehicleModel>>().Setup(m => m.Expression).Returns(vehicledata.Expression);
            mockSet.As<IQueryable<VehicleModel>>().Setup(m => m.ElementType).Returns(vehicledata.ElementType);
            mockSet.As<IQueryable<VehicleModel>>().Setup(m => m.GetEnumerator()).Returns(vehicledata.GetEnumerator());
            var p = new DbContextOptions<VehicleDbContext>();
            vehiclecontextmock = new Mock<VehicleDbContext>(p);
            vehiclecontextmock.Setup(x => x.Vehicles).Returns(mockSet.Object);



        }


        [Test]
        public void GetAllTest()
        {
            var vehiclerepo = new VehicleRepository(vehiclecontextmock.Object);
            var vehiclelist = vehiclerepo.GetAll();
            Assert.AreEqual(3, vehiclelist.Count());




        }

        [Test]
        public void GetAllTestFail()
        {
            var vehiclerepo = new VehicleRepository(vehiclecontextmock.Object);
            var vehiclelist = vehiclerepo.GetAll();
            Assert.AreNotEqual(2, vehiclelist.Count());




        }
        [Test]
        public void GetByIdTest()
        {
            var vehiclerepo = new VehicleRepository(vehiclecontextmock.Object);
            var vehicleobj = vehiclerepo.GetById(2);
            Assert.IsNotNull(vehicleobj);
        }
        [Test]
        public void GetByIdTestFail()
        {
            var vehiclerepo = new VehicleRepository(vehiclecontextmock.Object);
            var vehicleobj = vehiclerepo.GetById(10);
            Assert.IsNull(vehicleobj);
        }

    }

}
