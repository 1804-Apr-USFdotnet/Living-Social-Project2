﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RealEstateCRMConsumer.Controllers
{
    public class DataController : Controller
    {
        private static readonly HttpClient httpClient = new HttpClient();

        // GET: Data
        public async Task<ActionResult> Index()
        {
            HttpResponseMessage response = await httpClient.GetAsync("http://ec2-13-58-19-141.us-east-2.compute.amazonaws.com/realestateapi/api/Data/");
            TempData["message"] = response.ReasonPhrase;
            return View();
        }
    }
}