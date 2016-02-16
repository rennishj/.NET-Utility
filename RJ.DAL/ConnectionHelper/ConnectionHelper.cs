using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
