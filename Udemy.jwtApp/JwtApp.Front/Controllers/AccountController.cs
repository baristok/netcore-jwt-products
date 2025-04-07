using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using JwtApp.Front.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;

namespace JwtApp.Front.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }

        public ActionResult Login()
        {
            return View(new UserLoginModel());
        }

        [HttpPost]
        public async Task<ActionResult> Login(UserLoginModel userLoginModel)
        {
            if (ModelState.IsValid)
            {
                var client = this._httpClientFactory.CreateClient();
                var content = new StringContent(JsonSerializer.Serialize(userLoginModel), Encoding.UTF8,
                    "application/json");
                var response = await client.PostAsync("http://localhost:****/api/Auth/Login", content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    var tokenModel = JsonSerializer.Deserialize<JwtTokenResponseModel>(jsonData,
                        new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                        });
                    if (tokenModel != null)
                    {
                        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                        var token = handler.ReadJwtToken(tokenModel.Token);
                        var claims = token.Claims.ToList();
                        if (tokenModel.Token != null)
                            claims.Add(new Claim("accessToken", tokenModel.Token));

                        var claimsIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
                        var authProps = new AuthenticationProperties
                        {
                            ExpiresUtc = tokenModel.ExpireDate,
                            IsPersistent = true,
                        };
                        await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity), authProps);

                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı Adı veya Şifre Hatalıdır.");
                }

                return View();
            }

            return View(userLoginModel);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(JwtBearerDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
        
    }
}