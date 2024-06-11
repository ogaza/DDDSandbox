using ActiveRecord.Model;
using Microsoft.AspNetCore.Mvc;

namespace ActiveRecord.Web.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class BlogController : ControllerBase
  {
    // GET: api/blog/posts
    [HttpGet("Posts", Name = "GetPosts")]
    public async Task<IEnumerable<Post>> GetPosts()
    {
      return await Task.FromResult(Post.GetAll());
    }
  }
}
