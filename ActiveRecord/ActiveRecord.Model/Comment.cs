namespace ActiveRecord.Model
{
  public class Comment
  {
    public int Id { get; set; }

    public Post? Post { get; set; }

    public string? Text { get; set; }

    public string? Author { get; set; }

    public DateTime? Created { get; set; }
  }
}
