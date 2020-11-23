using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingService.Models;
using BookingService.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BookingDbContext dbContext;

        public BookingRepository(BookingDbContext _dbContext)
        {
            this.dbContext = _dbContext;
        }
        public BookingModel Book(BookingModel model)
        {
            var result = dbContext.Bookings.Add(model);
            dbContext.SaveChanges();
            return model;
        }
        public IEnumerable<BookingModel> GetById(int user_id)
        {
            return dbContext.Bookings.Where(b => b.User_Id == user_id).ToList();
        }
    }
}
