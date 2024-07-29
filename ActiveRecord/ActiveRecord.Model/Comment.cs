using System.Data.SqlClient;

namespace ActiveRecord.Model
{

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
