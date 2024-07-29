using System.Data.SqlClient;

namespace ActiveRecord.Model
{
  public class Post : ActiveRecord<Post>
  {
    public string? Subject { get; set; }

    public string? Text { get; set; }

    public IList<Comment>? Comments { get; set; }

    public DateTime? Created { get; set; }

    public static IEnumerable<Post> GetAllWithComments()
    {
      Post? elem = null;
      var list = new List<Post>();

      string queryString = @"
        SELECT 
          P.*,
          C.*
        FROM
          Posts P
        INNER JOIN 
          Comments C ON C.PostId = P.Id
      ";

      using (SqlConnection connection =
        new(DBConfiguration.ConnectionString))
      {
        connection.Open();

        SqlCommand command = new(queryString, connection);
        SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
          if (elem?.Id != (int)reader[0])
          {
            elem = new()
            {
              Comments = []
            };
            elem.GetFromReader(reader);
            list.Add(elem);
          }

          var comment  = GetCommentFromReader(reader);
          elem.Comments.Add(comment);
        }
        reader.Close();
      }
      return list;
    }

    protected override void GetFromReader(SqlDataReader reader)
    {
      Id = (int)reader[0];
      Subject = (string)reader[1];
      Text = (string)reader[2];
      Created = (DateTime?)reader[3];
    }

    protected static Comment GetCommentFromReader(SqlDataReader reader)
    {
      var id = reader[4];
      var text = reader[5];
      var author = reader[6];
      var created = reader[8];

      var idInt = id == DBNull.Value ? null : (int?)id;
      var textStr = text == DBNull.Value ? null : (string)text;
      var authorStr = author == DBNull.Value ? null : (string)author;
      var createdDate = created == DBNull.Value ? null : (DateTime?)created;

      return new Comment
      {
        Id = idInt,
        Text = textStr,
        Author = authorStr,
        Created = createdDate
      };
    }
  }
}
