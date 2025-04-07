using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using JwtApp.Front.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Serialization;

namespace JwtApp.Front.Controllers;

[Authorize(Roles = "Admin,Member")]
public class ProductController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ProductController(IHttpClientFactory httpClientFactory)
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
            var response = await client.GetAsync("http://localhost:****/api/Products");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<List<ProductListModel>>(jsonData,
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
            await client.DeleteAsync($"http://localhost:****/api/Products/{id}");
        }

        return RedirectToAction("List");
    }

    public async Task<IActionResult> Create()
    {
        var model = new CreateProductModel();

        var token = User.Claims.FirstOrDefault(c => c.Type == "accessToken")?.Value;
        if (token != null)
        {
            var client = this._httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync("http://localhost:****/api/Categories/");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<List<CategoryListModel>>(jsonData, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
                model.Categories = new SelectList(data, "Id", "Definition");
                return View(model);
            }
        }

        return RedirectToAction("List");
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateProductModel model)
    {
        var data = TempData["Categories"]?.ToString();
        if (data != null)
        {
            var categories = JsonSerializer.Deserialize<List<SelectListItem>>(data);
            model.Categories = new SelectList(categories, "Value", "Text");
        }

        if (ModelState.IsValid)
        {
            var token = User.Claims.FirstOrDefault(c => c.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = this._httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var jsonData = JsonSerializer.Serialize(model);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("http://localhost:****/api/Products/", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("List");
                }

                ModelState.AddModelError("", "Bir Hata Oluştu.");
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

            var responseProduct = await client.GetAsync($"http://localhost:****/api/Products/{id}");
            if (responseProduct.IsSuccessStatusCode)
            {
                var jsonData = await responseProduct.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<UpdateProductModel>(jsonData,
                    new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });

                var responseCategory = await client.GetAsync($"http://localhost:****/api/Categories");
                if (responseCategory.IsSuccessStatusCode)
                {
                    var jsonCategoryData = await responseCategory.Content.ReadAsStringAsync();
                    var data = JsonSerializer.Deserialize<List<CategoryListModel>>(jsonCategoryData, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                    if (result != null)
                        result.Categories = new SelectList(data, "Id", "Definition");
                }


                return View(result);
            }
        }

        return RedirectToAction("List");
    }
    
    
    [HttpPost]
    public async Task<ActionResult> Update(UpdateProductModel model)
    {
        var data = TempData["Categories"]?.ToString();
        if (data != null)
        {
            var categories = JsonSerializer.Deserialize<List<SelectListItem>>(data);
            model.Categories = new SelectList(categories, "Value", "Text", model.CategoryId);
        }

        if (ModelState.IsValid)
        {
            var token = User.Claims.FirstOrDefault(c => c.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = this._httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var jsonData = JsonSerializer.Serialize(model);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var response = await client.PutAsync("http://localhost:****/api/Products/", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("List");
                }

                ModelState.AddModelError("", "Bir Hata Oluştu.");
            }
        }

        return View(model);
    }
}