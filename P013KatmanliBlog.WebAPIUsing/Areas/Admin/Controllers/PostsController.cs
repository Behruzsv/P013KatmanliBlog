using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using P013KatmanliBlog.Core.Entities;
using P013KatmanliBlog.WebAPIUsing.Utils;

namespace P013KatmanliBlog.WebAPIUsing.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Policy = "AdminPolicy")]
    public class PostsController : Controller
    {

        private readonly HttpClient _httpClient;
        private readonly string _apiAdres = "https://localhost:7010/api/Posts";
        private readonly string _apiAdresKategori = "https://localhost:7010/api/Categories";
        public PostsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        // GET: PostsController
        public async Task<ActionResult> Index()
        {
            var model = await _httpClient.GetFromJsonAsync<List<Post>>(_apiAdres);
            return View(model);
        }

        // GET: ProductsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductsController/Create
        public async Task<ActionResult> CreateAsync()
        {
            ViewBag.CategoryId = new SelectList(await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdresKategori), "Id", "Name");
            return View();
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Post collection, IFormFile? ImageUrl)
        {
            try
            {
                if (ImageUrl is not null)
                {
                    collection.ImageUrl = await FileHelper.FileLoaderAsync(ImageUrl);
                }
                var response = await _httpClient.PostAsJsonAsync(_apiAdres, collection);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }
            ViewBag.CategoryId = new SelectList(await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdresKategori), "Id", "Name");
            return View();
        }

        // GET: ProductsController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            ViewBag.CategoryId = new SelectList(await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdresKategori), "Id", "Name");
            var model = await _httpClient.GetFromJsonAsync<Post>(_apiAdres + "/" + id);
            return View(model);
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Post collection, IFormFile? Image, bool? resmiSil)
        {
            try
            {
                if (resmiSil is not null && resmiSil == true)
                {
                    FileHelper.FileRemove(collection.ImageUrl);
                    collection.ImageUrl = "";
                }
                if (Image is not null)
                {
                    collection.ImageUrl = await FileHelper.FileLoaderAsync(Image);
                }
                var response = await _httpClient.PutAsJsonAsync(_apiAdres, collection);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }
            ViewBag.CategoryId = new SelectList(await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdresKategori), "Id", "Name");
            return View();
        }

        // GET: ProductsController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<Post>(_apiAdres + "/" + id);
            return View(model);
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, Post collection)
        {
            try
            {
                FileHelper.FileRemove(collection.ImageUrl);
                await _httpClient.DeleteAsync(_apiAdres + "/" + id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}