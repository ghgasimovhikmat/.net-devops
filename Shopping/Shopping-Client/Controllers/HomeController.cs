using Microsoft.AspNetCore.Mvc;
using Shopping_Client.Data;
using Shopping_Client.Models;
using System.Diagnostics;
using Newtonsoft.Json;



namespace Shopping_Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;

        public HomeController(ILogger<HomeController> logger,IHttpClientFactory httpClientFactory)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpClient = httpClientFactory.CreateClient("ShoppingAPIClinet");
        }

        //public async Task< IActionResult> Index()
        //{
        //    var response = await _httpClient.GetAsync("/product");

        //    var content = await response.Content.ReadAsStringAsync();
        //    var productList = JsonConvert.DeserializeObject<IEnumerable<Product>>(content);


        //    //return View(ProductContext.Products);

        //    //var response = await _httpClient.GetAsync("/product");

        //    //var content = await response.Content.ReadAsStringAsync();
        //    //var productList = JsonConvert.DeserializeObject<IEnumerable<Product>>(content);

        //    return View(productList);
        //}
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/product");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                // Handle error response here
                // For example, return a specific view indicating the error
                return View("Error");
            }

            var productList = JsonConvert.DeserializeObject<IEnumerable<Product>>(content);

            if (productList == null)
            {
                // Handle case where productList is null
                // For example, return a specific view indicating no products are available
                return View("NoProducts");
            }

            return View(productList);
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
