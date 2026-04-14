using EONET.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace EONET.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(
            ILogger<HomeController> logger,
            IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        /// <summary>
        /// Index action that fetches data from the EONET API, deserializes it, and passes it to the view for display.
        /// </summary>
        /// <returns>A view displaying the list of events fetched from the EONET API.</returns>
        public async Task<IActionResult> Index()
        {
            using HttpClient client = new HttpClient();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string json = await client.GetStringAsync(_configuration["EonetApiUrl"]);

            var root = JsonSerializer.Deserialize<EonetRoot>(json, options);
            return View(root?.events ?? new List<Event>());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
