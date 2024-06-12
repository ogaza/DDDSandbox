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
        @"SELECT 
            Id, 
            Subject, 
            Text,
            Created
          from dbo.Posts";

      using (SqlConnection connection = 
        new(DBConfiguration.ConnectionString))
      {
        connection.Open();

        SqlCommand command = new(queryString, connection);
        SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
          Console.WriteLine(
            "\t{0}\t{1}\t{2}",
            reader[0], 
            reader[1], 
            reader[2]);

          list.Add(
            new Post 
            { 
              Id = (int)reader[0],
              Subject = (string)reader[1],
              Text = (string)reader[2],
              Created = (DateTime?)reader[3]
            });
        }
        reader.Close();
      }
      return list;
    }
  }
}
