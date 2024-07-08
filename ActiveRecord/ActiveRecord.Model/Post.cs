using System.Data.SqlClient;

namespace ActiveRecord.Model
{
  public class Post : ActiveRecord<Post>
  {
    public string? Subject { get; set; }

    public string? Text { get; set; }

    public IList<Comment>? Comments { get; set; }

    public DateTime? Created { get; set; }

    protected override void GetFromReader(SqlDataReader reader)
    {
      Id = (int)reader[0];
      Subject = (string)reader[1];
      Text = (string)reader[2];
      Created = (DateTime?)reader[3];
    }
  }
}
