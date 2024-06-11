namespace ActiveRecord.Model
{
  public class Post
  {
    public int Id { get; set; }

    public string? Subject { get; set; }

    public string? Text { get; set; }

    public IList<Comment>? Comments { get; set; }

    public DateTime? Created { get; set; }
  }
}
