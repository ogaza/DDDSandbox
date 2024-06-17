using System.Data.SqlClient;

namespace ActiveRecord.Model
{
  public class Post
  {
    public int Id { get; set; }

    public string? Subject { get; set; }

    public string? Text { get; set; }

    public IList<Comment>? Comments { get; set; }

    public DateTime? Created { get; set; }

    public static IEnumerable<Post> GetAll()
    {
      var list = new List<Post>();

      const string queryString =
        @"
        SELECT 
          Id, 
          Subject, 
          Text,
          Created
        FROM 
          Posts";

      using (SqlConnection connection = 
        new(DBConfiguration.ConnectionString))
      {
        connection.Open();

        SqlCommand command = new(queryString, connection);
        SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
          list.Add(GetFromReader(reader));
        }
        reader.Close();
      }
      return list;
    }

    protected static Post GetFromReader(SqlDataReader reader)
    {
      Post result = new()
      {
        Id = (int)reader[0],
        Subject = (string)reader[1],
        Text = (string)reader[2],
        Created = (DateTime?)reader[3]
      };

      return result;
    }
  }
}
