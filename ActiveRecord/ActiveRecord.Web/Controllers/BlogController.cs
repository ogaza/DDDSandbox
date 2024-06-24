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

    //[HttpDelete("Posts/{id:int}", Name = "DeletePost")]
    //public async Task<int?> DeletePost(int id)
    //{
    //  int? result = null;

    //  return await Task.FromResult(id);
    //}
    
    [HttpDelete("Comments/{id:int}", Name = "DeleteComment")]
    public async Task<bool> DeleteComment(int id)
    {
      var result = Comment.Delete(id);

      return await Task.FromResult(result);
    }

    [HttpPost("Comments", Name = "SaveComment")]
    public async Task<int> SaveComment(Comment comment)
    {
      var result = Comment.Save(comment);

      return await Task.FromResult(result);
    }
  }
}
