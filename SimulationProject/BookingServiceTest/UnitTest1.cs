using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using BookingService.Models;
using BookingService.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;

namespace BookingServiceTest
{
    public class Tests
    {

        List<BookingModel> books = new List<BookingModel>();
        IQueryable<BookingModel> bookingdata;
        Mock<DbSet<BookingModel>> mockSet;
        Mock<BookingDbContext> bookcontextmock;
        [SetUp]
        public void Setup()
        {
            books = new List<BookingModel>()
            {
                 new BookingModel{Booking_Id = 1, vehicle_Id=1,User_Id=1,BookingDate=DateTime.Parse("2020-05-05 00:00:00"),No_ofMonth =4,TotalCost=10000.00},
                 new BookingModel{Booking_Id = 1, vehicle_Id=1,User_Id=1,BookingDate= DateTime.Parse("2020-10-11 00:00:00"),No_ofMonth =1,TotalCost=2000.00}

            };
            bookingdata = books.AsQueryable();
            mockSet = new Mock<DbSet<BookingModel>>();
            mockSet.As<IQueryable<BookingModel>>().Setup(m => m.Provider).Returns(bookingdata.Provider);
            mockSet.As<IQueryable<BookingModel>>().Setup(m => m.Expression).Returns(bookingdata.Expression);
            mockSet.As<IQueryable<BookingModel>>().Setup(m => m.ElementType).Returns(bookingdata.ElementType);
            mockSet.As<IQueryable<BookingModel>>().Setup(m => m.GetEnumerator()).Returns(bookingdata.GetEnumerator());
            var p = new DbContextOptions<BookingDbContext>();
            bookcontextmock = new Mock<BookingDbContext>(p);
            bookcontextmock.Setup(x => x.Bookings).Returns(mockSet.Object);



        }


        [Test]
        public void GetAllBookingsByUserIdTest()
        {
            var bookingrepo = new BookingRepository(bookcontextmock.Object);
            var bookinglist = bookingrepo.GetById(1);
            Assert.AreEqual(2, bookinglist.Count());
        }

        [Test]
        public void GetAllBookingsByUserIdTestFail()
        {
            var bookingrepo = new BookingRepository(bookcontextmock.Object);
            var bookinglist = bookingrepo.GetById(1);
            Assert.AreNotEqual(1, bookinglist.Count());
        }

        [Test]
        public void AddBookingDetailTest()
        {
            var bookingrepo = new BookingRepository(bookcontextmock.Object);
            var bookingobj = bookingrepo.Book(new BookingModel { Booking_Id = 3, vehicle_Id = 1, User_Id = 1, BookingDate = DateTime.Parse("2020-03-03"), No_ofMonth = 1, TotalCost = 6000.00 });
            Assert.IsNotNull(bookingobj);
        }
    }
}
