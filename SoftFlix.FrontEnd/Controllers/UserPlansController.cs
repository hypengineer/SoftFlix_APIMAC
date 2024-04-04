using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SoftFlix.FrontEnd.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;

namespace SoftFlix.FrontEnd.Controllers
{
    public class UserPlansController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserPlansController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7133/api/Plans");

            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<PlanResponseModel>>(jsonData);
                return View(result);
            }
            else
            {
                return View(null);
            }

        }

        

        [HttpPost]
        public async Task<IActionResult> Create(short planId)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(planId);

            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
           // return RedirectToAction(,);
            var responseMessage = await client.PostAsync("https://localhost:7133/api/UserPlans?planId="+planId.ToString(), null);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index","Categories");
            }

            else
            {

                return RedirectToAction("Index");
            }

        }

    }
}
