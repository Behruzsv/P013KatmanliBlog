using Microsoft.AspNetCore.Mvc;
using P013KatmanliBlog.Core.Entities;
using P013KatmanliBlog.MVCUI.Models;
using P013KatmanliBlog.Service.Abstract;
using System.Diagnostics;

namespace P013KatmanliBlog.MVCUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IService<Post> _servicePost;
        private readonly IService<Category> _serviceCategory;

        public HomeController(IService<Post> servicePost, IService<Category> serviceCategory)
        {
            _servicePost = servicePost;
            _serviceCategory = serviceCategory;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var model = new HomePageViewModel()
            {
                Categories = await _serviceCategory.GetAllAsync(),
                Posts = await _servicePost.GetAllAsync(p => p.IsActive && p.IsHome)
            };
            return View(model);
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