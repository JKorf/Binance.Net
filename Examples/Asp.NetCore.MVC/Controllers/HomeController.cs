using System.Diagnostics;
using Asp.Net.Models;
using Binance.Net.Interfaces;
using Binance.Net.Interfaces.Clients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Asp.Net.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBinanceDataProvider _dataProvider;

        public HomeController(IBinanceDataProvider dataProvider)
        {
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
