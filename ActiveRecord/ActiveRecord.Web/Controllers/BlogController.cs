using Microsoft.AspNetCore.Mvc;

namespace ActiveRecord.Web.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class BlogController : ControllerBase
  {
    // GET: api/blog/posts
    [HttpGet("Posts", Name = "GetPosts")]
    public async Task<IEnumerable<string>> GetPosts()
    {
      var list = new []{ "1" };

      return await Task.FromResult(list);
    }
  }
}
