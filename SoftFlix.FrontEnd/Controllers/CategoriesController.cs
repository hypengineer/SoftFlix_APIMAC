using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SoftFlix.FrontEnd.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;

namespace SoftFlix.FrontEnd.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CategoriesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        
        public async Task<IActionResult> Index()
            {
                var client = _httpClientFactory.CreateClient();
                var responseMessage =await client.GetAsync("https://localhost:7133/api/Categories");

            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK) { 
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var result =JsonConvert.DeserializeObject<List<CategoryResponseModel>>(jsonData);
                return View(result);
            }
            else
            {
                return View(null);
            }
                
            }

        public IActionResult Create()
        {
           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryResponseModel model)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);

            StringContent content = new StringContent(jsonData,Encoding.UTF8,"application/json");
           var responseMessage = await client.PostAsync("https://localhost:7133/api/Categories", content);

            if(responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            else {
                TempData["errorMessage"] = $"Bir hata ile karşılaşıldı. Hata kodu : {(int)responseMessage.StatusCode}";
                    return View(model); }

        }
        
    }
}
