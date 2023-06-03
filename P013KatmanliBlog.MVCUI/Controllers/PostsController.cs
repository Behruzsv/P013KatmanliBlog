using Microsoft.AspNetCore.Mvc;
using P013KatmanliBlog.Core.Entities;
using P013KatmanliBlog.MVCUI.Models;
using P013KatmanliBlog.Service.Abstract;

namespace P013KatmanliBlog.MVCUI.Controllers
{
	public class PostsController : Controller
	{
		private readonly IPostService _servicePost;

		public PostsController(IPostService servicePost)
		{
			_servicePost = servicePost;
		}

		public async Task<IActionResult> DetailAsync(int id)
		{
			var model = new PostDetailViewModel();
			var post = await _servicePost.GetPostByIncludeAsync(id);
			model.Post = post;
			model.RelatePosts = await _servicePost.GetAllAsync(p => p.CategoryId == post.CategoryId && p.Id != id);
			if (model is null)
			{
				return NotFound();
			}
			return View(model);
		}
	}
}
