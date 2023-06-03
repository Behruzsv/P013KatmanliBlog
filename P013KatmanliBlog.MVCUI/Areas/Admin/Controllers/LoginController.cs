using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using P013KatmanliBlog.Core.Entities;
using P013KatmanliBlog.MVCUI.Models;
using P013KatmanliBlog.Service.Abstract;
using System.Security.Claims;

namespace P013KatmanliBlog.MVCUI.Areas.Admin.Controllers
{
    [Area("Admin")]
        public class LoginController : Controller
        {
            private readonly IService<AppUser> _service;

            public LoginController(IService<AppUser> service)
            {
                _service = service;
            }
            public IActionResult Index()
            {
                return View();
            }
            [Route("Logout")]
            public async Task<IActionResult> Logout()
            {
                await HttpContext.SignOutAsync(); 
                return Redirect("/Admin/Login"); 
            }
            [HttpPost]
            public async Task<IActionResult> IndexAsync(AdminLoginViewModel admin)
            {
                try
                {
                    var kullanici = await _service.GetAsync(k => k.IsActive && k.Email == admin.Email && k.Password == admin.Password);
                    if (kullanici != null)
                    {
                        var kullaniciYetkileri = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, kullanici.Email),
                        new Claim("Role", kullanici.IsAdmin ? "Admin" : "User"),
                        new Claim("UserGuid", kullanici.UserGuid.ToString())
                    };
                        var kullaniciKimligi = new ClaimsIdentity(kullaniciYetkileri, "Login");
                        ClaimsPrincipal principal = new(kullaniciKimligi);
                        await HttpContext.SignInAsync(principal);
                        return Redirect("/Admin/Main");
                    }
                    else

                        ModelState.AddModelError("", "Giriş Başarısız!");

                }
                catch (Exception hata)
                {
                    ModelState.AddModelError("", "Hata Oluştu!" + hata.Message);
                }
                return View();
            }
        }
    }

