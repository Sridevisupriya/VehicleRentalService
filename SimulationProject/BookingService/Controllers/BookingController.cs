using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingService.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookingService.Models;

namespace BookingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(BookingController));
      
        private readonly IBookingRepository _bookingRepository;

        public BookingController(IBookingRepository bookingRepository)
        {
            this._bookingRepository = bookingRepository;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {

                _log4net.Info("Get BookingDetails by Id accessed");
                var bookinglist = _bookingRepository.GetById(id);
                return new OkObjectResult(bookinglist);

            }
            catch
            {
                _log4net.Error("Error in getting Booking Details");
                return new NoContentResult();
            }
        }

        [HttpPost]
        public IActionResult  PostBookVehicle(BookingModel model)
        {
            try
            {
                _log4net.Info("Book Details Getting Added");
                if (ModelState.IsValid)
                {
                    _bookingRepository.Book(model);
                    return CreatedAtAction(nameof(PostBookVehicle), new { id = model.Booking_Id }, model);

                }
                return BadRequest();

            }
            catch
            {
                _log4net.Error("Error in Adding Booking Details");
                return new NoContentResult();

            }
            
           


          
          
        }

    }
}