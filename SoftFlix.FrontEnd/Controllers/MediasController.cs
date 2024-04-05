using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SoftFlix.FrontEnd.Models;

namespace SoftFlix.FrontEnd.Controllers
{
    public class MediasController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MediasController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7133/api/Medias");

            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<MediaResponseModel>>(jsonData);
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
        public async Task<IActionResult> Create(MediaResponseModel model)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);

            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7133/api/Medias", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            else
            {
                TempData["errorMessage"] = $"Bir hata ile karşılaşıldı. Hata kodu : {(int)responseMessage.StatusCode}";
                return View(model);
            }

        }
    }
}