﻿using System.Collections.Generic;
using System.Data.SqlClient;

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
