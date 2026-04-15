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
        public async Task<IActionResult> Index(string sortOrder)
        {
            using HttpClient client = new HttpClient();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            string json = await client.GetStringAsync(_configuration["EonetApiUrl"]);
            var root = JsonSerializer.Deserialize<EonetRoot>(json, options);

            //Splits title into name, county amd state.
            var viewModel = root?.events.Select(item =>
            {
                var parts = (item.title ?? "").Split(',', StringSplitOptions.TrimEntries);

                return new EonetData
                {
                    _name = parts.Length > 0 ? parts[0] : "",
                    _county = parts.Length > 1 ? parts[1] : "",
                    _state = parts.Length > 2 ? parts[2] : "",
                    _link = item.link ?? ""
                };
            }).ToList() ?? new List<EonetData>();

            viewModel = sortOrder switch
            {
                "nameDesc" => viewModel.OrderByDescending(x => x._name).ToList(),
                "county" => viewModel.OrderBy(x => x._county).ToList(),
                "countyDesc" => viewModel.OrderByDescending(x => x._county).ToList(),
                "state" => viewModel.OrderBy(x => x._state).ToList(),
                "stateDesc" => viewModel.OrderByDescending(x => x._state).ToList(),
                _ => viewModel.OrderBy(x => x._name).ToList()
            };


            return View(viewModel);
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
