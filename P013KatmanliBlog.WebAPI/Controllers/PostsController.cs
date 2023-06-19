using Microsoft.AspNetCore.Mvc;
using P013KatmanliBlog.Core.Entities;
using P013KatmanliBlog.Service.Abstract;

namespace P013KatmanliBlog.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PostsController : ControllerBase
	{
		private readonly IPostService _service;

		public PostsController(IPostService service)
		{
			_service = service;
		}
		// GET: api/<PostsController>
		[HttpGet]
		public async Task<IEnumerable<Post>> GetAsync()
		{
			return await _service.GetPostsByIncludeAsync();
		}

		// GET api/<PostsController>/5
		[HttpGet("{id}")]
		public async Task<Post> GetAsync(int id)
		{
			return await _service.GetPostByIncludeAsync(id);
		}

        [HttpGet("GetSearch/{q}")]
        public async Task<IEnumerable<Post>> GetSearchAsync(string q)
        {
            return await _service.GetPostsByIncludeAsync(p => p.IsActive && p.Title.Contains(q) || p.Content.Contains(q) ||p.Category.Name.Contains(q));
        }

        // POST api/<PostsController>
        [HttpPost]
		public async Task<IActionResult> PostAsync([FromBody] Post value)
		{
			await _service.AddAsync(value);
			await _service.SaveAsync();
			return Ok(value);
		}

		// PUT api/<PostsController>/5
		[HttpPut]
		public async Task<IActionResult> Put([FromBody] Post value)
		{
			_service.Update(value);
			var sonuc = await _service.SaveAsync();
			if (sonuc > 0)
			{
				return Ok(value);
			}
			return Problem();
		}

		// DELETE api/<PostsController>/5
		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteAsync(int id)
		{
			var data = await _service.FindAsync(id);
			if (data == null)
			{
				return NotFound();
			}
			_service.Delete(data);
			var sonuc = await _service.SaveAsync();
			if (sonuc > 0)
			{
				return Ok(data);
			}
			return Problem();
		}
	}
}
