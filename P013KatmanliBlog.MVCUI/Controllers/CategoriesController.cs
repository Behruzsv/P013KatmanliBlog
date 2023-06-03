using Microsoft.AspNetCore.Mvc;
using P013KatmanliBlog.Core.Entities;
using P013KatmanliBlog.Service.Abstract;

namespace P013KatmanliBlog.MVCUI.Controllers
{
	public class CategoriesController : Controller
	{
		private readonly IService<Category> _service;
		private readonly IPostService _servicePost;

		public CategoriesController(IService<Category> service, IPostService servicePost)
		{
			_service = service;
			_servicePost = servicePost;
		}

		public async Task<IActionResult> IndexAsync(int id)
		{
			var model = await _service.FindAsync(id);
			if (model == null)
			{
				return NotFound();
			}
			model.Posts = await _servicePost.GetAllAsync(p => p.CategoryId == id);
			return View(model);
		}
	}
}
