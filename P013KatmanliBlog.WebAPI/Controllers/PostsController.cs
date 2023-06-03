using Microsoft.AspNetCore.Mvc;
using P013KatmanliBlog.Core.Entities;
using P013KatmanliBlog.Service.Abstract;

namespace P013KatmanliBlog.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PostsController : ControllerBase
	{
		private readonly IService<Post> _service; // readonly nesneler sadece constructor metotta doldurulabilir
		public PostsController(IService<Post> service)
		{
			_service = service;
		}
		// GET: api/<PostsController>
		[HttpGet]
		public async Task<IEnumerable<Post>> GetAsync()
		{
			return await _service.GetAllAsync();
		}

		// GET api/<PostsController>/5
		[HttpGet("{id}")]
		public async Task<Post> GetAsync(int id)
		{
			return await _service.FindAsync(id);
		}

		// GET api/<PostsController>/5
		[HttpGet("{id}")]
		public async Task<Post> PostAsync([FromBody]Post value)       
		{
			await _service.AddAsync(value);
			await _service.SaveAsync();
			return value;
		}

		// PUT api/<PostsController>/5
		[HttpPut("{id}")]
		public async Task<ActionResult> PutAsync(int id, [FromBody] Post value)
		{
			_service.Update(value);
			int sonuc = await _service.SaveAsync();
			if (sonuc > 0)
			{
				return Ok(value);
			}
			return StatusCode(StatusCodes.Status304NotModified);
		}

		// DELETE api/<PostsController>/5
		[HttpDelete("{id}")]
		public ActionResult Delete(int id)
		{
			var kayit = _service.Find(id);
			if (kayit == null)
			{
				return NoContent();
			}
			_service.Delete(kayit);
			_service.Save();
			return Ok(kayit);
		}
	}
}
