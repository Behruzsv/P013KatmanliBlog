using Microsoft.AspNetCore.Mvc;
using P013KatmanliBlog.Core.Entities;
using P013KatmanliBlog.WebAPIUsing.Models;

namespace P013KatmanliBlog.WebAPIUsing.Controllers
{
	public class PostsController : Controller
	{
		private readonly HttpClient _httpClient;

		public PostsController(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		private readonly string _apiAdres = "https://localhost:7010/api/Posts";
		[Route("tum-haberler")]
		public async Task<IActionResult> IndexAsync()
		{
			var model = await _httpClient.GetFromJsonAsync<List<Post>>(_apiAdres);
			return View(model);
		}

        public async Task<IActionResult> Search(string q) // adres çubuğunda query string ile 
        {
            var posts = await _httpClient.GetFromJsonAsync<List<Post>>(_apiAdres + "/GetSearch/" + q);

            return View(posts);
        }

        public async Task<IActionResult> DetailAsync(int id)
        {
            var model = new PostDetailViewModel();
            var posts = await _httpClient.GetFromJsonAsync<List<Post>>(_apiAdres);
            var post = await _httpClient.GetFromJsonAsync<Post>(_apiAdres + "/" + id);
            model.Post = post;
            model.RelatePosts = posts.Where(p => p.CategoryId == post.CategoryId && p.Id != id).ToList();
            if (model is null)
            {
                return NotFound();
            }
            return View(model);
        }
    }
}
