using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SoftFlix.FrontEnd.Models;
using System.Net.Http;
using System.Text;

namespace SoftFlix.FrontEnd.Controllers
{
    public class UsersController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UsersController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginResponseModel model)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);

            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7133/api/SoftFlixUsers/LogIn", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index","UserPlans");
            }

            else
            {
                TempData["errorMessage"] = $"Bir hata ile karşılaşıldı. Hata kodu : {(int)responseMessage.StatusCode}";
                return View(model);
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterResponseModel model)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);

            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7133/api/SoftFlixUsers/Register", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Login");
            }

            else
            {
                TempData["errorMessage"] = $"Bir hata ile karşılaşıldı. Hata kodu : {(int)responseMessage.StatusCode}";
                return View(model);
            }
        }
    }
}
