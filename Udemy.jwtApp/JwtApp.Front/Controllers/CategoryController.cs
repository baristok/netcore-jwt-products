using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using JwtApp.Front.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace JwtApp.Front.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ActionResult> List()
        {
            var token = User.Claims.FirstOrDefault(c => c.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = this._httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetAsync("localhost:****/api/Categories");

                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<List<CategoryListModel>>(jsonData,
                        new JsonSerializerOptions()
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                        });
                    return View(result);
                }
            }


            return View();
        }

        public async Task<ActionResult> Remove(int id)
        {
            var token = User.Claims.FirstOrDefault(c => c.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = this._httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                await client.DeleteAsync($"http://localhost:****/api/Categories/{id}");
            }

            return RedirectToAction("List");
        }

        public IActionResult Create()
        {
            return View(new CreateCategoryModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var token = User.Claims.FirstOrDefault(c => c.Type == "accessToken")?.Value;
                if (token != null)
                {
                    var client = this._httpClientFactory.CreateClient();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var jsonData = JsonSerializer.Serialize(model);
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("http://localhost:****/api/Categories",content);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("List");
                    }
                    else
                    {
                        ModelState.AddModelError("","Bir Hata Oluştu.");
                    }
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Update(int id)
        {
            var token = User.Claims.FirstOrDefault(c => c.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = this._httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                
                var response = await client.GetAsync($"http://localhost:****/api/Categories/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<CategoryListModel>(jsonData,
                        new JsonSerializerOptions()
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                        });
                    return View(result);
                }
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryListModel model)
        {
            if (ModelState.IsValid)
            {
                var token = User.Claims.FirstOrDefault(c => c.Type == "accessToken")?.Value;
                if (token != null)
                {
                    var client = this._httpClientFactory.CreateClient();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var jsonData = JsonSerializer.Serialize(model);
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    var response = await client.PutAsync("http://localhost:****/api/Categories",content);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("List");
                    }
                    else
                    {
                        ModelState.AddModelError("","Bir Hata Oluştu.");
                    }
                }
            }
            return View(model);
        }
            
    }
}