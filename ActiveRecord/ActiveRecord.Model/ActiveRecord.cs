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
}
