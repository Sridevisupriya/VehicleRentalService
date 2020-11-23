using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.Models
{
    public class BookingModel
    {
        [Key]
        public int Booking_Id { get; set; }
        public int User_Id { get; set; }
        public int vehicle_Id { get; set; }

        public DateTime BookingDate { get; set; }
        public int No_ofMonth { get; set; }
        public double TotalCost { get; set; }
    }
}
