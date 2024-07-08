using System.Data.SqlClient;

namespace ActiveRecord.Model
{
  public abstract class ActiveRecord<E> where E : ActiveRecord<E>, new()
  {
    protected static string QueryString_GetAll
    {
      get
      {
        return
        @"
        SELECT
          *
        FROM "
          + $"{typeof(E).Name}s ";
      }
    }
    protected static string QueryString_Get
    {
      get
      {
        return
        @"
        SELECT
          *
        FROM "
          + $"{typeof(E).Name}s " +
        //+ nameof(E) +
        @"
        WHERE
          Id = @id";
      }
    }

    protected static string QueryString_Delete
    {
      get
      {
        return
        @"
        DELETE
        FROM "
          + $"{typeof(E).Name}s " +
        //+ nameof(E) +
        @"
        WHERE
          Id = @id";
      }
    }

    public int? Id { get; set; }

    public static IEnumerable<E> GetAll()
    {
      E elem;
      var list = new List<E>();

      string queryString = QueryString_GetAll;

      using (SqlConnection connection =
        new(DBConfiguration.ConnectionString))
      {
        connection.Open();

        SqlCommand command = new(queryString, connection);
        SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
          elem = new();
          elem.GetFromReader(reader);
          list.Add(elem);
        }
        reader.Close();
      }
      return list;
    }

    public static E GetById(int id)
    {
      E result = new();
       
      using (SqlConnection connection =
        new(DBConfiguration.ConnectionString))
      {
        connection.Open();

        string qstr = QueryString_Get;
        SqlCommand command = new(qstr, connection);

        command.Parameters.Add(new SqlParameter("@id", id));

        SqlDataReader reader = command.ExecuteReader();

        if (reader.Read())
        {
          result = new E();
          result.GetFromReader(reader);
        }

        reader.Close();
      }
      return result;
    }

    public static bool Delete(int id)
    {
      int rowsAffected = 0;

      string queryString = QueryString_Delete;

      using (SqlConnection connection =
        new(DBConfiguration.ConnectionString))
      {
        connection.Open();
        SqlCommand command = new(queryString, connection);

        command.Parameters.Add(new SqlParameter("@id", id));

        rowsAffected = command.ExecuteNonQuery();
      }

      return rowsAffected > 0;
    }

    protected abstract void GetFromReader(SqlDataReader reader);
  }

  public class Comment : ActiveRecord<Comment>
  {
    public Post? Post { get; set; }

    public string? Text { get; set; }

    public string? Author { get; set; }

    public DateTime? Created { get; set; }

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
        OUTPUT 
          INSERTED.Id
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

        result = (int)command.ExecuteScalar();
      }

      return result;
    }

    protected override void GetFromReader(SqlDataReader reader)
    {
      Id = (int)reader[nameof(Id)];
      Text = (string)reader[nameof(Text)];
      Author = (string)reader[nameof(Author)];
      Created = (DateTime?)reader[nameof(Created)];
    }
  }
}
