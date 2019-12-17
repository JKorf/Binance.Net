﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Binance.Net.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBinanceClient _binanceClient;
        private readonly IBinanceDataProvider _dataProvider;

        public HomeController(ILogger<HomeController> logger, IBinanceClient client, IBinanceDataProvider dataProvider)
        {
            _logger = logger;
            _binanceClient = client;
            _dataProvider = dataProvider;
        }

        public IActionResult Index()
        {
            return View(_dataProvider.LastKline);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
