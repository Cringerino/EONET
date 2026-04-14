using EONET.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;

namespace EONET.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(
            ILogger<HomeController> logger,
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var apiUrl = _configuration["EonetApiUrl"];
            if (string.IsNullOrWhiteSpace(apiUrl))
            {
                _logger.LogError("Missing configuration value for EonetApiUrl.");
                return View(new List<EonetEvent>());
            }

            var client = _httpClientFactory.CreateClient();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            try
            {
                var response = await client.GetFromJsonAsync<EonetResponse>(apiUrl, options);
                return View(response?.Events ?? new List<EonetEvent>());
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Failed to fetch data from the EONET API.");
                return View(new List<EonetEvent>());
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Failed to deserialize data from the EONET API.");
                return View(new List<EonetEvent>());
            }
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
