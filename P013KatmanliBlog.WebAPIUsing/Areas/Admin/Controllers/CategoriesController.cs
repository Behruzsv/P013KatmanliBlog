﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using P013KatmanliBlog.Core.Entities;
using P013KatmanliBlog.WebAPIUsing.Utils;


namespace P013KatmanliBlog.WebAPIUsing.Areas.Admin.Controllers
{
	[Area("Admin"), Authorize(Policy = "AdminPolicy")]
	public class CategoriesController : Controller
	{
		private readonly HttpClient _httpClient;
		private readonly string _apiAdres = "https://localhost:7010/api/Categories";
		public CategoriesController(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		// GET: CategoriesController
		public async Task<ActionResult> Index()
		{
			var model = await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdres);
			return View(model);
		}

		// GET: CategoriesController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: CategoriesController/Create
		public async Task<ActionResult> CreateAsync()
		{
			ViewBag.ParentId = new SelectList(await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdres), "Id", "Name");
			return View();
		}

		// POST: CategoriesController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> CreateAsync(Category collection, IFormFile? Image)
		{
			try
			{
				if (Image is not null)
				{
					collection.Image = await FileHelper.FileLoaderAsync(Image);
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
			ViewBag.ParentId = new SelectList(await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdres), "Id", "Name");
			return View();
		}

		// GET: CategoriesController/Edit/5
		public async Task<ActionResult> Edit(int id)
		{
			ViewBag.ParentId = new SelectList(await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdres), "Id", "Name");
			var model = await _httpClient.GetFromJsonAsync<Category>(_apiAdres + "/" + id);
			return View(model);
		}

		// POST: CategoriesController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Category collection, IFormFile? Image, bool? resmiSil)
        {
            try
            {
                if (resmiSil is not null && resmiSil == true)
                {
                    FileHelper.FileRemove(collection.Image);
                    collection.Image = "";
                }
                if (Image is not null)
                {
                    collection.Image = await FileHelper.FileLoaderAsync(Image);
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
            ViewBag.ParentId = new SelectList(await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdres), "Id", "Name");
            return View();
        }

        // GET: CategoriesController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
		{
			var model = await _httpClient.GetFromJsonAsync<Category>(_apiAdres + "/" + id);
			return View(model);
		}

		// POST: CategoriesController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteAsync(int id, Category collection)
		{
			try
			{
				FileHelper.FileRemove(collection.Image);
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
