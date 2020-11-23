using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingService.Models;

namespace BookingService.Repository
{
    public interface IBookingRepository
    {
        public IEnumerable<BookingModel> GetById(int booking_id);
        BookingModel Book(BookingModel entity);
    }
}
