using System.Buffers.Text;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using ECOMMERCE.Models;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.Mvc;

namespace ECOMMERCE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        const string baseUrl = "https://fakestoreapi.com/";
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        #region Index View Method's
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetDetails(string url)
        {
            try
            {
                if (string.IsNullOrEmpty(url))
                {
                    _logger.LogWarning("Given Url is Null");
                    return Json(new { message = "Error", data = url });
                }

                var httpClient = new HttpClient();
                HttpResponseMessage response = httpClient.GetAsync(baseUrl + url).Result;
                if (response.IsSuccessStatusCode)
                {
                    string stateInfo = response.Content.ReadAsStringAsync().Result;
                    var retJson = new { message = response.ReasonPhrase, data = stateInfo };
                    return Json(retJson);
                }
                else
                {
                    string stateInfo = response.Content.ReadAsStringAsync().Result;
                    var retJson = new { message = response.ReasonPhrase, data = stateInfo };
                    return Json(retJson);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Json(new { message = "Exception", data = ex.Message });
            }

        }



        #endregion


        #region Default's


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #endregion
    }
}
