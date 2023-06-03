using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using P013KatmanliBlog.Core.Entities;
using P013KatmanliBlog.MVCUI.Utils;
using P013KatmanliBlog.Service.Abstract;
using System.Drawing.Drawing2D;

namespace P013KatmanliBlog.MVCUI.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Policy = "AdminPolicy")]
    public class PostsController : Controller
    {
        private readonly IPostService _service;
        private readonly IService<Category>
_serviceCategory;

        public PostsController(IPostService service, IService<Category> serviceCategory)
        { 
            _service = service;
            _serviceCategory = serviceCategory;
        }

        // GET: ProductsController
        public async Task<ActionResult> Index()
        {
            var model = await _service.GetPostsByIncludeAsync();
            return View(model);
        }

        // GET: ProductsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductsController/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.CategoryId = new SelectList(await _serviceCategory.GetAllAsync(), "Id", "Name");
            return View();

        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Post collection, IFormFile? Image)
        {
            try
            {
                if (Image is not null)
                {
                    collection.ImageUrl = await FileHelper.FileLoaderAsync(Image);
                }
                await _service.AddAsync(collection);
                await _service.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştur!");
            }
            ViewBag.CategoryId = new SelectList(await _serviceCategory.GetAllAsync(), "Id", "Name");
            return View();
        }

        // GET: ProductsController/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var model = await _service.FindAsync(id.Value);
            if (model == null)
            {
                return NotFound();
            }
            ViewBag.CategoryId = new SelectList(await _serviceCategory.GetAllAsync(), "Id", "Name");
            return View(model);
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Post collection, IFormFile? ImageUrl, bool? resmiSil)
        {
            try
            {
                if (resmiSil is not null && resmiSil == true)
                {
                    FileHelper.FileRemove(collection.ImageUrl);
                    collection.ImageUrl = "";
                }
                if (ImageUrl is not null)
                {
                    collection.ImageUrl = await FileHelper.FileLoaderAsync(ImageUrl);
                }
                _service.Update(collection);
                await _service.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştur!");
            }
            ViewBag.CategoryId = new SelectList(await _serviceCategory.GetAllAsync(), "Id", "Name");
            return View();

        }

        // GET: ProductsController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var model = await _service.GetPostByIncludeAsync(id);
            return View(model);
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Post collection)
        {
            try
            {
                _service.Delete(collection);
                _service.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
