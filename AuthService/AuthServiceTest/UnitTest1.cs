using System.Collections.Generic;
using System.Linq;
using System.Net;
using AuthService.Controllers;
using AuthService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;

namespace AuthServiceTest
{
    public class Tests
    {
        List<Customer> user = new List<Customer>();
        IQueryable<Customer> userdata;
        Mock<DbSet<Customer>> mockSet;
        Mock<CustomerDbContext> usercontextmock;
        [SetUp]
        public void Setup()
        {
            user = new List<Customer>()
            {
                new Customer{Customer_Name="Lakshmi",Password="Lakshmi@1"}

            };
            userdata = user.AsQueryable();
            mockSet = new Mock<DbSet<Customer>>();
            mockSet.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(userdata.Provider);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(userdata.Expression);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(userdata.ElementType);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(userdata.GetEnumerator());
            var p = new DbContextOptions<CustomerDbContext>();
            usercontextmock = new Mock<CustomerDbContext>(p);
            usercontextmock.Setup(x => x.Customers).Returns(mockSet.Object);



        }


        [Test]
        public void LoginTest()
        {

            Mock<IConfiguration> config = new Mock<IConfiguration>();
            config.Setup(p => p["Jwt:Key"]).Returns("ThisismySecretKey");
            var controller = new CustomerController(usercontextmock.Object, config.Object);
            var login = controller.Login(new Customer { Customer_Name = "Lakshmi", Password = "Lakshmi@1" });


            var Actualstatuscode = (HttpStatusCode)login.GetType().GetProperty("StatusCode").GetValue(login);

            var ExpectedStatusCode = HttpStatusCode.OK;
            Assert.AreEqual(ExpectedStatusCode, Actualstatuscode);
        }
        [Test]
        public void LoginFailTest()
        {

            Mock<IConfiguration> config = new Mock<IConfiguration>();
            config.Setup(p => p["Jwt:Key"]).Returns("ThisismySecretKey");
            var controller = new CustomerController(usercontextmock.Object, config.Object);
            var login = controller.Login(new Customer { Customer_Name = "Meghana", Password = "Meghana@1" });
            var Actualstatuscode = (HttpStatusCode)login.GetType().GetProperty("StatusCode").GetValue(login);
            var ExpectedStatusCode = HttpStatusCode.NotFound;

            Assert.AreEqual(ExpectedStatusCode, Actualstatuscode);
        }

    }
}