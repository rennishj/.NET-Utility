using System.Configuration;
using System.Data.SqlClient;

namespace RJ.DAL.ConnectionHelper
{
  public class ConnectionHelper
    {
      public static SqlConnection GetConnectionString()
      {
          return new SqlConnection(ConfigurationManager.ConnectionStrings["NetUtility"].ConnectionString);
      }
    }
}
