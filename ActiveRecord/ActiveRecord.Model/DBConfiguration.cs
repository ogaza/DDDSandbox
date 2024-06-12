namespace ActiveRecord.Model
{
  public static class DBConfiguration
  {
    private static bool _initialised = false;
    private static string? _connectionString { get; set; }

    public static void SetConnectionString(string connectionString)
    {
      if (_initialised) return;

      _connectionString = connectionString;
      _initialised = true;
    }

    public static string? ConnectionString => 
      _connectionString;
  }
}
