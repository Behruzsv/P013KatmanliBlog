using Microsoft.AspNetCore.Authorization;
using P013KatmanliBlog.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace P013KatmanliBlog.WebAPIUsing.Areas.Admin.Controllers
{
	[Area("Admin"), Authorize(Policy = "AdminPolicy")]

    public class AppUsersController : Controller
    {
        private readonly HttpClient _httpClient; 
        private readonly string _apiAdresi = "https://localhost:7010/api/AppUsers"; 

        public AppUsersController(HttpClient httpClient)
        {
            _httpClient = httpClient; 
                                      
        }

        // GET: AppUsersController
        public async Task<ActionResult> Index()
        {
            var model = await _httpClient.GetFromJsonAsync<List<AppUser>>(_apiAdresi); 
            return View(model);
        }

        // GET: AppUsersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AppUsersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AppUsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(AppUser collection)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(_apiAdresi, collection);
                if (response.IsSuccessStatusCode) 
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }
            return View(collection);
        }

        // GET: AppUsersController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<AppUser>(_apiAdresi + "/" + id);
            return View(model);
        }

        // POST: AppUsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, AppUser collection)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync(_apiAdresi, collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }
            return View();
        }

        // GET: AppUsersController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<AppUser>(_apiAdresi + "/" + id);
            return View();
        }

        // POST: AppUsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, IFormCollection collection)
        {
            try
            {
                await _httpClient.DeleteAsync(_apiAdresi + "/" + id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

