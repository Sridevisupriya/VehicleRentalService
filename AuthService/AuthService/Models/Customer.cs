using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Models
{
    public class Customer
    { 
        [Key]
        public int Customer_Id { get; set; }
        public string Customer_Name { get; set; }
        public string Password { get; set; }
    }
}
