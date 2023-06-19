using Microsoft.AspNetCore.Mvc;
using P013KatmanliBlog.Core.Entities;
using P013KatmanliBlog.WebAPIUsing.Models;
using System.Diagnostics;

namespace P013KatmanliBlog.WebAPIUsing.Controllers
{
	public class HomeController : Controller
	{
		private readonly HttpClient _httpClient;

		public HomeController(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		private readonly string _apiAdres = "https://localhost:7010/api/";

        public async Task<IActionResult> IndexAsync()
        {
            var posts = await _httpClient.GetFromJsonAsync<List<Post>>(_apiAdres + "Posts");
            var model = new HomePageViewModel()
            {
                Posts = posts.Where(p => p.IsActive && p.IsHome).ToList()
            };
            return View(model);
        }
        
        [Route("AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}