﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VehicleClientApplication.Models;

namespace VehicleClientApplication.Controllers
{
    public class LoginController : Controller
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(LoginController));

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Customer user)
        {
            _log4net.Info("User Login");
            Customer Item = new Customer();
            using (var httpClient = new HttpClient())
            {
                // StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                //var response = await httpClient.PostAsync("https://localhost:44399/api/AuthService/Customer", content);
                //string apiResponse = await response.Content.ReadAsStringAsync();
                //Item = JsonConvert.DeserializeObject<Customer>(apiResponse);
                StringContent content1 = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                using (var response1 = await httpClient.PostAsync("https://localhost:44399/api/Customer/Login", content1))
                {
                    if (!response1.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Login");
                    }

                    string apiResponse1 = await response1.Content.ReadAsStringAsync();



                    string stringJWT = response1.Content.ReadAsStringAsync().Result;


                    JWT jwt = JsonConvert.DeserializeObject<JWT>(stringJWT);

                    HttpContext.Session.SetString("token", jwt.Token);
                    HttpContext.Session.SetString("user", JsonConvert.SerializeObject(user));
                    HttpContext.Session.SetInt32("Customer_Id", user.Customer_Id );
                    HttpContext.Session.SetString("Customer_Name", user.Customer_Name);
                    ViewBag.Message = "User logged in successfully!";

                    return RedirectToAction("Index", "VehicleList");


                }
            }
        }
        public ActionResult Logout()
        {
            _log4net.Info("User Log Out");
            HttpContext.Session.Remove("token");
            // HttpContext.Session.SetString("user", null);

            return View("Login");
        }
    }
}
