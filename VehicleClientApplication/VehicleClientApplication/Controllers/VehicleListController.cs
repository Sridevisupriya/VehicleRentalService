using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VehicleClientApplication.Models;

namespace VehicleClientApplication.Controllers
{
    public class VehicleListController : Controller
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(VehicleListController));
        
        public IActionResult Index2()
        {
            return View();
        }
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                _log4net.Info("token not found");

                return RedirectToAction("Login");

            }
            else
            {
                _log4net.Info("Productlist getting Displayed");

                List<Vehicle> ItemList = new List<Vehicle>();
                using (var client = new HttpClient())
                {


                    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);

                    client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

                    using (var response = await client.GetAsync("https://localhost:44337/api/Vehicle"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        ItemList = JsonConvert.DeserializeObject<List<Vehicle>>(apiResponse);
                    }
                }
                return View(ItemList);

            }
        }

        public async Task<IActionResult> Book(int id)
        {
            _log4net.Info("Booking in progess");
            if (HttpContext.Session.GetString("token") == null)
            {

                return RedirectToAction("Login", "Login");

            }
            else
            {

                Vehicle Item = new Vehicle();
                Booking b = new Booking();
                using (var client = new HttpClient())
                {


                    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);

                    client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

                    using (var response = await client.GetAsync("https://localhost:44337/api/Vehicle/"+id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        Item = JsonConvert.DeserializeObject<Vehicle>(apiResponse);
                    }
                    b.vehicle_Id = Item.Vehicle_Id;
                    b.User_Id = Convert.ToInt32(HttpContext.Session.GetInt32("Customer_Id"));
                    b.BookingDate = DateTime.Now;
                    b.No_ofMonth = 0;
                    b.TotalCost = 0;
                }
                return View(b);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Book(Booking b)
        {
            _log4net.Info("Booking Done");
            if (HttpContext.Session.GetString("token") == null)
            {

                return RedirectToAction("Login", "Login");

            }
            else
            {

                Vehicle p = new Vehicle();

                using (var client = new HttpClient())
                {
                    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);

                    client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

                    using (var response = await client.GetAsync("https://localhost:44337/api/Vehicle/" + b.vehicle_Id))
                    {

                        string apiResponse = await response.Content.ReadAsStringAsync();
                        p = JsonConvert.DeserializeObject<Vehicle>(apiResponse);
                    }

                    b.TotalCost = b.No_ofMonth * p.Rent_per_month;
                    b.User_Id = Convert.ToInt32(HttpContext.Session.GetInt32("Customer_Id"));




                    StringContent content = new StringContent(JsonConvert.SerializeObject(b), Encoding.UTF8, "application/json");

                    using (var response = await client.PostAsync("https://localhost:44374/api/Booking/", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        b = JsonConvert.DeserializeObject<Booking>(apiResponse);
                    }
                }
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> GetBookingItems(int id)
        {
            _log4net.Info("Getting Booking details");
            if (HttpContext.Session.GetString("token") == null)
            {

                return RedirectToAction("Login", "Login");

            }
            else
            {
                List<Booking> item = new List<Booking>();
                ViewBag.Customer_Name = Convert.ToString(HttpContext.Session.GetString("Customer_Name"));
                var name = Convert.ToString(HttpContext.Session.GetString("Customer_Name"));


                using (var client = new HttpClient())
                {


                    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);
                    if (HttpContext.Session.GetInt32("Customer_Id") != null)
                    {
                        id = Convert.ToInt32(HttpContext.Session.GetInt32("Customer_Id"));
                    }

                        client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

                    using (var response = await client.GetAsync("https://localhost:44374/api/Booking/" + id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        item = JsonConvert.DeserializeObject<List<Booking>>(apiResponse);
                    }
                }
                return View(item);
            }


        }
    }
}