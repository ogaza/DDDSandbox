using System.Collections.Generic;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace ActiveRecord.Model
{
  public class Comment
  {
    public int Id { get; set; }

    public Post? Post { get; set; }

    public string? Text { get; set; }

    public string? Author { get; set; }

    public DateTime? Created { get; set; }

    public static IEnumerable<Comment> GetAll()
    {
      var list = new List<Comment>();

      const string queryString =
        @"
        SELECT
          Id,
          Text,
          Author,
          PostId,
          Created
        FROM 
          Comments";

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

    public static Comment GetById(int id)
    {
      Comment result;

      const string queryString =
        @"
        SELECT
          Id,
          Text,
          Author,
          PostId,
          Created
        FROM 
          Comments
        WHERE
          Id = @id";

      using (SqlConnection connection =
        new(DBConfiguration.ConnectionString))
      {
        connection.Open();

        SqlCommand command = new(queryString, connection);

        command.Parameters.Add(new SqlParameter("@id", id));

        SqlDataReader reader = command.ExecuteReader();

        if (reader.Read())
        {
          result = GetFromReader(reader);
        }
        else
        {
          result = new Comment();
        }

        reader.Close();
      }
      return result;
    }

    public static bool Delete(int id)
    {
      int rowsAffected = 0;

      const string queryString =
        @"
        DELETE
          Comments
        WHERE 
          Id = @id";

      using (SqlConnection connection =
        new(DBConfiguration.ConnectionString))
      {
        connection.Open();
        SqlCommand command = new(queryString, connection);

        command.Parameters.Add(new SqlParameter("@id", id));

        rowsAffected =  command.ExecuteNonQuery();
      }

      return rowsAffected > 0;
    }

    public static int Save(Comment comment) 
    {
      int result = 0;

      const string queryString =
      @"
      IF(@Id IS NULL)
        INSERT INTO
          Comments
          (
            Text,
            Author,
            PostId,
            Created
          )
        OUTPUT INSERTED.Id
        VALUES
        (
          @Text,
          @Author,
          @PostId,
          @Created
        )
      ELSE
        UPDATE
          Comments
        SET
          Text = @Text,
          Author = @Author,
          PostId = @PostId,
          Created = @Created
        WHERE
          Id = @Id";

      using (SqlConnection connection =
        new(DBConfiguration.ConnectionString))
      {
        connection.Open();

        SqlCommand command = new(queryString, connection);

        if (comment.Id > 0)
        {
          command.Parameters.Add(new SqlParameter("@Id", comment.Id));
        }
        else 
        {
          command.Parameters.Add(new SqlParameter("@Id", DBNull.Value));
        }
        command.Parameters.Add(new SqlParameter("@Text", comment.Text ?? ""));
        command.Parameters.Add(new SqlParameter("@Author", comment.Author ?? ""));
        command.Parameters.Add(new SqlParameter("@PostId", comment?.Post?.Id ?? 0));
        command.Parameters.Add(new SqlParameter("@Created", comment?.Created ?? DateTime.Now));

        if (comment?.Id > 0)
        {
          result = command.ExecuteNonQuery();
        }
        else
        {
          result = (int)command.ExecuteScalar();
        }
      }

      return result;
    }

    protected static Comment GetFromReader(SqlDataReader reader)
    {
      Comment result = new()
      {
        Id = (int)reader[0],
        Text = (string)reader[1],
        Author = (string)reader[2],
        Created = (DateTime?)reader[3]
      };

      return result;
    }
  }
}
